using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistCheckpointManager : MonoBehaviour
{
    public void ShowCheckpoints()
    {
        foreach(Scientist s in GetComponentsInChildren<Scientist>())
        {
            foreach (Transform t in s.Checkpoints)
            {
                t.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    public void HideCheckpoints()
    {
        foreach (Scientist s in GetComponentsInChildren<Scientist>())
        {
            foreach (Transform t in s.Checkpoints)
            {
                t.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
