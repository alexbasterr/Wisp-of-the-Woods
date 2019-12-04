﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations.Utilities;

public class characterMovement : MonoBehaviour
{
    public List<GameObject> detectionManager = new List<GameObject>();
    private waypoints waitPointsManager;
    public bool detectado;
    public bool escondido;
    public KeyCode TeclaSilvar;

    public Vector3 checkpoint;
    public GameObject panelGameOver;
    public bool menuPausa;
    public bool interactuarArbustos;
    public bool saltar;
    public bool aullar;
    public GameObject aullarGameObject;
    public GameObject exclamacion;
    public MalbersAnimations.MFreeLookCamera camara;
    public GameObject arbusto;

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

                if(interactuarArbustos)
                {
                    if (!saltar && Input.GetKeyDown(KeyCode.Space))
                    {
                        saltar = true;
                        transform.GetChild(3).gameObject.SetActive(false);
                        camara.SetTarget(arbusto);
                        GetComponent<CharacterMovement1>().enabled = false;

                    }
                    else if (saltar && Input.GetKeyDown(KeyCode.Space))
                    {
                        saltar = false;
                        transform.GetChild(3).gameObject.SetActive(true);
                        camara.SetTarget(transform.GetChild(0).transform);
                        GetComponent<CharacterMovement1>().enabled = true;
                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
                    }
                }

                if (aullar)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        GetComponent<Animator>().SetBool("Aullar", true);
                        GetComponent<MalbersAnimations.Controller.MAnimal>().lockMovement.Value = true;
                        GetComponent<CharacterMovement1>().enabled = false;
                    }
                }

                foreach (var item in detectionManager)
                {
                    if (Input.GetKeyDown(TeclaSilvar) && item.GetComponent<DetectionManager>().detectable)
                    {
                        GetComponent<Animator>().speed = 2;
                        Silvar();
                        aullar = true;
                        GetComponent<Animator>().SetBool("Aullar", true);
                        GetComponent<MalbersAnimations.Controller.MAnimal>().lockMovement.Value = true;
                        GetComponent<CharacterMovement1>().enabled = false;
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

    public void desactivarAullar()
    {
        GetComponent<Animator>().speed = 1;
        aullar = false;
        GetComponent<MalbersAnimations.Controller.MAnimal>().lockMovement.Value = false;
        GetComponent<Animator>().SetBool("Aullar", false);
        GetComponent<CharacterMovement1>().enabled = true;
    }
    public void desactivarAullarPiedra()
    {
        aullarGameObject.GetComponent<Texto_emergente>().desactivar();
        aullarGameObject.SetActive(false);
        exclamacion.SetActive(true);
    }

    public void Silvar()
    {
        foreach (var detector in detectionManager)
        {
            if (detector.GetComponent<DetectionManager>().detectable && !aullar)
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
