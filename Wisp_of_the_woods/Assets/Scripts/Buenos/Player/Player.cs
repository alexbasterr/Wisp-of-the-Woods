using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public float maxSpeed;
    float speed;

    public Transform camara;
    public Transform centroCamara;
    public Transform modelo;

    public Vector2 input;
    public Vector3 direc;
    public Transform Enemy;
    public Transform arbusto;
    Transform posicionInicialArbusto;
    Transform posicionInicial;
    public bool entrandoArbusto;
    public bool dentroArbusto;
        
    Animator anim;
    float contador;

    public bool grounded;
    public bool detectado;
    private Vector3 posCur;
    private Vector3 turnVector;
    private Quaternion rotCur;
            

    private void Awake()
    {
        speed = maxSpeed;
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;                
    }

    private void Update()
    {

        Contador();
        Movement();
        Rotation();
        if(input.sqrMagnitude != 0)
            DetectFloor();
        if(detectado)
            LookAtEnemy();
        if(arbusto)
            Esconderse();

        if (dentroArbusto)
            SalirArbusto();


    }

    void SalirArbusto()
    {
        if (Input.GetButtonDown("Interactuar"))
        {
            transform.GetChild(1).localEulerAngles -= new Vector3(0, 180, 0);
            if (entrandoArbusto && dentroArbusto)
                StartCoroutine(salirArbusto(0.5f));
        }
    }
    IEnumerator salirArbusto(float tiempo)
    {
        entrandoArbusto = false;
        arbusto.GetComponent<Arbusto>().ActivarAnimacion();
        anim.SetTrigger("saltar");
        Vector3 posicion = transform.GetChild(1).localPosition;
        for (int i = 0; i <= 50; i++)
        {
            transform.GetChild(1).localPosition = new Vector3(Mathf.Lerp(posicion.x, 0, i * 0.02f), Mathf.Lerp(posicion.y, 0.163f, i * 0.02f), Mathf.Lerp(posicion.z, 0, i * 0.02f));
            yield return new WaitForSeconds(tiempo * 0.02f);
        }
        dentroArbusto = false;
        yield return null;
    }

    void Esconderse()
    {
        if(Input.GetButtonDown("Interactuar"))
        {
            transform.GetChild(1).LookAt(arbusto);
            transform.GetChild(1).localEulerAngles = new Vector3(0, transform.GetChild(1).localEulerAngles.y, 0);
            if (!entrandoArbusto && !dentroArbusto)
                StartCoroutine(entrarArbusto(2));
        }
    }
    

    IEnumerator entrarArbusto(float tiempo)
    {
        entrandoArbusto = true;
        arbusto.GetComponent<Arbusto>().ActivarAnimacion();
        posicionInicialArbusto = transform.GetChild(1).transform;
        posicionInicial = transform.GetChild(1);
        anim.SetTrigger("saltar");

        for (int i = 0; i <= 50; i++)
        {
            transform.GetChild(1).position = new Vector3(Mathf.Lerp(posicionInicialArbusto.position.x, arbusto.position.x, i * 0.02f), Mathf.Lerp(posicionInicialArbusto.position.y, arbusto.position.y+1, i * 0.02f), Mathf.Lerp(posicionInicialArbusto.position.z, arbusto.position.z, i * 0.02f));
            yield return new WaitForSeconds(tiempo * 0.02f);
        }
        dentroArbusto = true;
        yield return null;
    }


    void LookAtEnemy()
    {
        transform.GetChild(1).LookAt(Enemy);
        transform.GetChild(1).localEulerAngles = new Vector3(0, transform.GetChild(1).localEulerAngles.y, 0);
        anim.SetBool("sentar", true);
    }

    public void DetectFloor()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.5f) == true)
        {
            Debug.DrawLine(transform.position, hit.point, Color.green);
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

        if (grounded && input.sqrMagnitude == 0)
            GetComponent<Rigidbody>().useGravity = false;
        else
            GetComponent<Rigidbody>().useGravity = true;
    }

    private void LateUpdate()
    {
        UpdateAnimations();
    }

    public void Movement()
    {

        if (canMove())
        {
            direc = new Vector3(modelo.localEulerAngles.x, centroCamara.localEulerAngles.y, modelo.localEulerAngles.z);
            input = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        }
        else
        {
            input = Vector2.zero;
            direc = Vector3.zero;
        }

        transform.Translate(input.x * speed * Time.deltaTime * camara.forward);
        transform.Translate(input.y * speed * Time.deltaTime * camara.right);
    }

    public bool canMove()
    {
        if (Aullando() || Saltando() || detectado || dentroArbusto)
            return false;

        return true;
    }

    public void Rotation()
    {
        //Rotation in move
        if (input.sqrMagnitude != 0)
        {
            anim.SetBool("andar", true);
            if (input.x == 1 && input.y == 1)
                modelo.localEulerAngles = new Vector3(direc.x, direc.y + 45, direc.z);
            else if (input.x == 1 && input.y == 0)
                modelo.localEulerAngles = new Vector3(direc.x, direc.y, direc.z);
            else if (input.x == 1 && input.y == -1)
                modelo.localEulerAngles = new Vector3(direc.x, direc.y - 45, direc.z);
            else if (input.x == 0 && input.y == 1)
                modelo.localEulerAngles = new Vector3(direc.x, direc.y + 90, direc.z);
            else if (input.x == 0 && input.y == -1)
                modelo.localEulerAngles = new Vector3(direc.x, direc.y - 90, direc.z);
            else if (input.x == -1 && input.y == 0)
                modelo.localEulerAngles = new Vector3(direc.x, direc.y + 180, direc.z);
            else if (input.x == -1 && input.y == 1)
                modelo.localEulerAngles = new Vector3(direc.x, direc.y + 135, direc.z);
            else if (input.x == -1 && input.y == -1)
                modelo.localEulerAngles = new Vector3(direc.x, direc.y + 225, direc.z);
        }
        else
            anim.SetBool("andar", false);
    }

    public void UpdateAnimations()
    {
        if (Input.GetKeyDown(KeyCode.E) && !Aullando())
        {
            anim.SetTrigger("aullar");
            speed = 0;
        }

        if ( Input.GetKeyDown(KeyCode.Space) && !Saltando())
        {            
                anim.SetTrigger("saltar");

            if (input.sqrMagnitude == 0)
                speed = 0;
        }

        if (contador >= 10 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Sentar"))
            anim.SetBool("sentar", true);
        else if(contador == 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("Sentar"))
            anim.SetBool("sentar", false);
    }

    public void Contador()
    {
        if (!Aullando() && !Saltando() && input.sqrMagnitude == 0)
            contador += Time.deltaTime;
        else
            contador = 0;
    }

    public bool Aullando()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("aullar"))
            return false;
        else
            return true;
    }

    public bool Saltando()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("saltar"))
            return false;
        else
            return true;
    }
    public void ResetSpeed()
    {
        speed = maxSpeed;
    }
}
