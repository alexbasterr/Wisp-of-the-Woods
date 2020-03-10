using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    public GameObject[] objects;
    public float minScale = 0.65f;
    public float maxScale = 0.8f;

    public void Scale()
    {
        foreach (GameObject go in objects)
        {
            float scaleFactor = Random.Range(minScale, maxScale);
            go.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

        }
    }
}
