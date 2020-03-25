using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scientist : MonoBehaviour
{
    #region Attributes

    NavMeshAgent agent;
    public DeteccionProximidad deteccionProximidad;
    public DeteccionSonido deteccionSonido;
    public Vision vision;

    #endregion

    #region Variables
    public List<Transform> Checkpoints = new List<Transform>();
    public int targetCheckpoint;
    public GameObject Player()
    {
        if(vision.player != null)
            return vision.player;
        else if (deteccionProximidad.player != null)
            return deteccionProximidad.player;

        return null;
    }

    private bool grounded;
    private Vector3 posCur;
    private Vector3 turnVector;
    private Quaternion rotCur;
    #endregion

    private void Awake()
    {
        //Asignar Componentes
        agent = GetComponent<NavMeshAgent>();
        vision = transform.GetChild(1).GetComponent<Vision>();
        deteccionProximidad = transform.GetChild(2).GetComponent<DeteccionProximidad>();
        deteccionSonido = transform.GetChild(3).GetComponent<DeteccionSonido>();

        //Añadir checkpoints sin tener que arrastrarlos
        Checkpoints = new List<Transform>();
        if (transform.parent.GetChild(1).childCount > 0)
        {
            Transform[] points = transform.parent.GetChild(1).GetComponentsInChildren<Transform>();
            for (int i = 0; i < points.Length; i++)
                Checkpoints.Add(points[i]);
            Checkpoints.RemoveAt(0);
        }
        targetCheckpoint = 0;

        //Establecer primer Checkpoint
        agent.SetDestination(Checkpoints[targetCheckpoint].position);
    }

    void Update()
    {
        //Orientarse con el suelo
        DetectFloor();

        //Patrol si no ha visto al jugador, sino ir hacia el
        if (!DetectadoPlayer() && !deteccionSonido.haOido)
            checkpointComplete();
        else if (deteccionSonido.haOido && !DetectadoPlayer())
            Investigar();
        else
            PlayerVisto();
    }

    public void DetectFloor()
    {
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) /*+ transform.forward / 2*/, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2) == true)
        {
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) /*+ transform.forward / 2*/, hit.point, Color.green);
            rotCur = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            posCur = new Vector3(transform.position.x, hit.point.y, transform.position.z);

            grounded = true;

        }
        else
            grounded = false;


        if (grounded == true)
        {
            transform.position = Vector3.Lerp(transform.position, posCur, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotCur, Time.deltaTime * 5);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, transform.position - Vector3.up * 1f, Time.deltaTime * 5);
            //from memory, I'm not sure why I aded this... Looks like a fail safe to me. When the object is turned too much towards teh front or back, almost instantly (*1000) make it rotate to a better orientation for aligning.
            if (transform.eulerAngles.x > 15)
            {
                turnVector.x -= Time.deltaTime * 1000;
            }
            else if (transform.eulerAngles.x < 15)
            {
                turnVector.x += Time.deltaTime * 1000;
            }
            //if we are not grounded, make the vehicle's rotation "even out". Not completey reaslistic, but easy to work with.
            rotCur.eulerAngles = Vector3.zero;
            transform.rotation = Quaternion.Lerp(transform.rotation, rotCur, Time.deltaTime);

        }
    }

    void Investigar()
    {
        StartCoroutine(InvestigarRuido());
    }

    IEnumerator InvestigarRuido()
    {
        Quaternion.FromToRotation(transform.forward,deteccionSonido.player.position);

        yield return new WaitForSeconds(0.02f);

        agent.SetDestination(deteccionSonido.player.position);
        
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            StartCoroutine(MirarAlrededor());
        }
    }
    IEnumerator MirarAlrededor()
    {
        yield return new WaitForSeconds(2);

        if (!DetectadoPlayer())
        {
            deteccionSonido.haOido = false;
            yield return null;
        }
    }
    public bool DetectadoPlayer()
    {
        if (Player() != null)
            return true;
        else
            return false;
    }

    public void checkpointComplete()
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            if (targetCheckpoint < Checkpoints.Count - 1)
                targetCheckpoint++;
            else
                targetCheckpoint = 0;
            
            agent.SetDestination(Checkpoints[targetCheckpoint].position);
        }
    }

    void PlayerVisto()
    {
        agent.SetDestination(Player().transform.position);
    }
}
