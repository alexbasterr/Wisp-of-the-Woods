using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    public float velocidad;
    public GameObject camara;
    public List<GameObject> detectionManager = new List<GameObject>();
    private waypoints waitPointsManager;
    public bool detectado;
    public bool escondido;
    public KeyCode TeclaSilvar;

    public Vector3 checkpoint;
    public GameObject panelGameOver;

    public bool menuPausa;
    private float rotacionActualHorizontal;
    private float rotacionActualVertical;

    private void Awake()
    {
        checkpoint = transform.position;
        for(int i = 0; i < FindObjectsOfType<DetectionManager>().Length; i++)
        {
            detectionManager.Add(FindObjectsOfType<DetectionManager>()[i].gameObject);
        }
        
        waitPointsManager = FindObjectOfType<waypoints>().GetComponent<waypoints>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public bool grounded;
    private Vector3 posCur;
    private Quaternion rotCur;
    public  GameObject posRay;

    private void FixedUpdate()
    {
        if (!menuPausa)
        {
            if (!detectado && !quieto)
            {
                
                /*else
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position - Vector3.up * 1f, Time.deltaTime * 5);
                    if (transform.eulerAngles.x > 15)
                    {
                        rotCur.x -= Time.deltaTime * 1000;
                    }
                    else if (transform.eulerAngles.x < 15)
                    {
                        rotCur.x += Time.deltaTime * 1000;
                    }
                    rotCur.eulerAngles = Vector3.zero;
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotCur, Time.deltaTime);

                }*/
            }
        }
    }

    private bool quieto;
    void Update()
    {
        /*if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0 && grounded)
        {
            quieto = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
           // GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            quieto = false;
            GetComponent<Rigidbody>().useGravity = true;
        }
        */

        if (!menuPausa)
        {
            if (!detectado)
            {
                
               /* if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal")!=0)
                        GetComponentInChildren<Animator>().SetBool("walk", true);*/
                //else GetComponentInChildren<Animator>().SetBool("walk", false);
                /*if(Input.GetKeyDown(KeyCode.Space))
                {
                    GetComponentInChildren<Animator>().SetTrigger("jump");
                }*/
             /*   transform.Translate(camara.transform.forward * Input.GetAxis("Vertical") * velocidad * Time.deltaTime, Space.World);
                transform.Translate(camara.transform.right * Input.GetAxis("Horizontal") * velocidad * Time.deltaTime, Space.World);*/

                //print(rotacionActualHorizontal);
               // transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, rotacionActualHorizontal, transform.localEulerAngles.z);
            }
            if (Input.GetKeyDown(TeclaSilvar))
            {
                Silvar();
            }
            if (detectado)
            {
                panelGameOver.SetActive(true);
            }

            if(Time.timeScale == 1)
            {
                GetComponent<Animator>().enabled = true;
                GetComponent<CharacterMovement1>().enabled = true;
            }
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!menuPausa)
            {
                Time.timeScale = 0;
                menuPausa = true;
                GetComponent<Animator>().enabled = false;
                GetComponent<CharacterMovement1>().enabled = false;
            }
        }
    }


    public void Silvar()
    {
        foreach (var detector in detectionManager)
        {
            if (detector.GetComponent<DetectionManager>().detectable)
            {
                detector.GetComponent<DetectionManager>().detectadoOido = true;
                detector.GetComponent<waypoints>().posicionOido = transform.position;
            }
        }
    }
    

    public void ResetearEnemigos()
    {
        foreach (var detector in detectionManager)
        {
            detector.GetComponent<DetectionManager>().detectable = false;
            detector.GetComponent<DetectionManager>().detectadoOido = false;
            detector.GetComponent<DetectionManager>().detectadoVisual = false;
        }
    }
    
}
