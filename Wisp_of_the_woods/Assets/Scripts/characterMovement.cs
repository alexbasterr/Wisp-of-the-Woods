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

    void Update()
    {
        if (!menuPausa)
        {
            if (!detectado)
            {
                
              
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
