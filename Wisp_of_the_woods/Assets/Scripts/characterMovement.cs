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
        
        //waitPointsManager = FindObjectOfType<waypoints>().GetComponent<waypoints>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!menuPausa)
        {
            if (!detectado)
            {
                
              
            }
            else
            {
                panelGameOver.SetActive(true);
                ResetearEnemigos();
                GetComponent<Animator>().enabled = true;
                GetComponent<CharacterMovement1>().enabled = true;
            }
            if (Input.GetKeyDown(TeclaSilvar))
            {
                Silvar();
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
