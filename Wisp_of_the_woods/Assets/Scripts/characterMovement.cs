using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    public List<GameObject> detectionManager = new List<GameObject>();
    private waypoints waitPointsManager;
    public bool detectado;
    public bool escondido;
    public bool interactuarArbusto;
    public KeyCode TeclaSilvar;

    public Vector3 checkpoint;
    public GameObject panelGameOver;

    public bool menuPausa;

    public GameObject arbusto;

    public MalbersAnimations.MFreeLookCamera camara;

    private void Awake()
    {
        checkpoint = transform.position;
        for(int i = 0; i < FindObjectsOfType<DetectionManager>().Length; i++)
        {
            detectionManager.Add(FindObjectsOfType<DetectionManager>()[i].gameObject);
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!menuPausa)
        {
            if (!detectado)
            {
                if (Input.GetKeyDown(TeclaSilvar))
                {
                    Silvar();
                }

                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
                {
                    if (!menuPausa)
                    {
                        Time.timeScale = 0;
                        menuPausa = true;
                        GetComponent<Animator>().enabled = false;
                        GetComponent<CharacterMovement1>().enabled = false;
                    }
                }

                if(interactuarArbusto)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && !escondido)
                    {
                        camara.SetTarget(arbusto.transform.GetChild(0).transform);
                        arbusto.transform.GetChild(1).gameObject.transform.position = transform.position;
                        arbusto.transform.GetChild(1).gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
                        transform.position = arbusto.transform.GetChild(2).transform.position;
                        gameObject.transform.GetChild(3).gameObject.SetActive(false);
                        GetComponent<CharacterMovement1>().enabled = false;
                        GetComponent<Animator>().enabled = false;
                        escondido = true;
                    }
                    else if(escondido && Input.GetKeyDown(KeyCode.Space))
                    {
                        camara.SetTarget(transform.GetChild(0).transform);
                        transform.position = arbusto.transform.GetChild(1).transform.position;
                        transform.rotation = arbusto.transform.GetChild(1).transform.rotation;
                        gameObject.transform.GetChild(3).gameObject.SetActive(true);
                        GetComponent<CharacterMovement1>().enabled = true;
                        GetComponent<Animator>().enabled = true;
                        escondido = false;
                    }
                }

                

            }
            else
            {
                panelGameOver.SetActive(true);
                ResetearEnemigos();
                GetComponent<Animator>().enabled = true;
                GetComponent<CharacterMovement1>().enabled = true;
            }
            

            foreach (var item in detectionManager)
            {
                if(item.GetComponent<DetectionManager>().detectadoVisual)
                {
                    GetComponent<Animator>().enabled = false;
                    GetComponent<CharacterMovement1>().enabled = false;
                }
            }
        }


        
    }

    public void reactivarScripts()
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<CharacterMovement1>().enabled = true;
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
            detector.GetComponent<waypoints>().navMeshAgent.isStopped = false;
            detector.GetComponent<waypoints>().enabled = false;
            detector.GetComponent<waypoints>().enabled = true;
            detector.GetComponent<DetectionManager>().detectable = false;
            detector.GetComponent<DetectionManager>().detectadoOido = false;
            detector.GetComponent<DetectionManager>().detectadoVisual = false;
        }
    }
}
