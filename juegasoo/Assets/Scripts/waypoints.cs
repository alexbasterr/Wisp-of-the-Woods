﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class waypoints : MonoBehaviour
{

    public NavMeshAgent navMeshAgent;

    public Transform[] checkpoint;
    public int m_CurrentWaypointIndex;
    private DetectionManager detectionManager;
    public Vector3 posicionOido;
    public GameObject player;
    public bool investigando;
    public int contador;
    public bool rotandoPositivo;
    public float auxRot;

    private float velocidadInvestigar = 20;
    private void Awake()
    {
        detectionManager = GetComponent<DetectionManager>();
    }

    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(checkpoint[0].position);
    }
    

    void Update()
    {
        

        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance && !detectionManager.detectadoVisual && !detectionManager.detectadoOido)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % checkpoint.Length;
            navMeshAgent.SetDestination(checkpoint[m_CurrentWaypointIndex].transform.position);
        }
        else if(detectionManager.detectadoOido)
        {
            navMeshAgent.SetDestination(posicionOido);
            navMeshAgent.stoppingDistance = 4;

            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                investigando = true;
            }
        }

        if (detectionManager.detectadoVisual)
        {
            navMeshAgent.SetDestination(player.transform.position);
            navMeshAgent.stoppingDistance = 3.5f;
        }

        if (investigando)
        {
            StartCoroutine(Investigar());
        }
    }

    public IEnumerator  Investigar()
    {
        yield return new WaitForSeconds(2);
        detectionManager.detectadoOido = false;
        investigando = false;
    }

    public void rotarPositivo()
    {
        transform.Rotate(0, 1 * Time.deltaTime * velocidadInvestigar, 0);
    }
    public void rotarNegativo()
    {
        transform.Rotate(0, -1 * Time.deltaTime * velocidadInvestigar, 0);
    }
    
}
