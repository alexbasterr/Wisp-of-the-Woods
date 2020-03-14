using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public GameObject camara;

    private void Awake()
    {
        if (!Camera.main)
        {
            GameObject camera = Instantiate(camara);
            camera.name = "Cámara";
        }

    }
}
