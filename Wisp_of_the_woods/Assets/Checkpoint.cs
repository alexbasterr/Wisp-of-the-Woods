using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointNumber;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (other.GetComponent<PlayerCanvasController>().actualChekpoint < checkpointNumber)
                other.GetComponent<PlayerCanvasController>().actualChekpoint = checkpointNumber;
        }
    }
}
