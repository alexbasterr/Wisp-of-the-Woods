using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
    Idle,
    Patrol,
    Investigate,
    ChasePlayer
}

public class Scientist : MonoBehaviour
{
    #region Attributes
    Animator anim;
    NavMeshAgent agent;
    public DeteccionProximidad deteccionProximidad;
    public DeteccionSonido deteccionSonido;
    public Vision vision;

    #endregion

    #region Variables
    public EnemyState enemyState;

    public List<Transform> Checkpoints = new List<Transform>();
    public int targetCheckpoint;
    public float waitpointStopTime;
    bool playerDetected;
    public Vector3 playerPos;

    bool grounded;
    Vector3 posCur;
    Vector3 turnVector;
    Quaternion rotCur;
    #endregion

    void Awake()
    {
        //Asignar Componentes
        anim = GetComponent<Animator>();
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
        enemyState = EnemyState.Patrol;
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        agent.isStopped = false;
        anim.SetBool("Walk",true);
        agent.SetDestination(Checkpoints[targetCheckpoint].position);

        yield return new WaitUntil(() => agent.isStopped);
        anim.SetBool("Walk", false);
        yield return new WaitForSeconds(waitpointStopTime);
        if (enemyState == EnemyState.Patrol)
        {
            if (targetCheckpoint != Checkpoints.Count - 1)
                targetCheckpoint++;
            else
                targetCheckpoint = 0;
            StartCoroutine(Patrol());
        }
    }
    IEnumerator Investigate(Vector3 soundPosition)
    {
        deteccionSonido.haOido = false;
        agent.isStopped = true;
        anim.SetBool("Walk", false);
        yield return new WaitForSeconds(1);
        agent.isStopped = false;
        anim.SetBool("Walk", true);
        agent.SetDestination(soundPosition);
        yield return new WaitUntil(() => agent.isStopped);
        anim.SetBool("Walk", false); 
        yield return new WaitForSeconds(waitpointStopTime);
        if(!playerDetected)
        {
            enemyState = EnemyState.Patrol;
            StartCoroutine(Patrol());
        }
    }

    IEnumerator ChasePlayer()
    {
        playerDetected = true;
        Vector3 position;
        position = FindObjectOfType<PlayerMovement>().transform.position;

        agent.isStopped = false;
        anim.SetBool("Walk", true);
        agent.SetDestination(position);

        yield return new WaitUntil(() => agent.isStopped);

        anim.SetBool("Walk", false);

        yield return new WaitForSeconds(waitpointStopTime);
    }

    private void Update()
    {
        if (agent.stoppingDistance >= agent.remainingDistance)
            agent.isStopped = true;

        if (!playerDetected)
        {
            if (deteccionSonido.puedeOir && !deteccionSonido.haOido)
                playerPos = deteccionSonido.player.position;

            if (vision.visto || deteccionProximidad.visto)
            {
                agent.stoppingDistance = 2;
                StopAllCoroutines();
                enemyState = EnemyState.ChasePlayer;
                StartCoroutine(ChasePlayer());
            }

            if (deteccionSonido.haOido)
            {
                StopAllCoroutines();
                enemyState = EnemyState.Investigate;
                StartCoroutine(Investigate(playerPos));
            }
        }
    }

    void FixedUpdate()
    {
        //Orientarse con el suelo
        DetectFloor();
    }
    void DetectFloor()
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
}
