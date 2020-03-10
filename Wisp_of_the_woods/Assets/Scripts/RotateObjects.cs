using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjects : MonoBehaviour
{
    public GameObject[] objects;
    public float minYRotation = 0f;
    public float maxYRotation = 360f;

    public float minZRotation = -5f;
    public float maxZRotation = 5;

    public float minXRotation = -5f;
    public float maxXRotation = 5;


    public void Rotate()
    {
        foreach (GameObject go in objects)
        {
            go.transform.eulerAngles = new Vector3(Random.Range(minXRotation, maxXRotation), Random.Range(minYRotation, maxYRotation), Random.Range(minZRotation, maxZRotation));

        }
    }
}
