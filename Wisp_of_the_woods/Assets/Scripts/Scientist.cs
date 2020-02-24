using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scientist : MonoBehaviour
{
    NavMeshAgent agent;

    public List<Transform> Checkpoints = new List<Transform>();
    int targetCheckpoint;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Checkpoints = new List<Transform>();
        //Añadir checkpoints sin tener que arrastrarlos
        if (transform.parent.GetChild(1).childCount > 0)
        {
            Transform[] points = transform.parent.GetChild(1).GetComponentsInChildren<Transform>();
            for (int i = 0; i < points.Length; i++)
                Checkpoints.Add(points[i]);
            Checkpoints.RemoveAt(0);
        }
        
        targetCheckpoint = 0;
        agent.SetDestination(Checkpoints[targetCheckpoint].position);
    }

    void Update()
    {
        checkpointComplete();
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
}
