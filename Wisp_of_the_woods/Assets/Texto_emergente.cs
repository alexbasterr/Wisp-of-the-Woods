using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texto_emergente : MonoBehaviour
{
    public GameObject go;

    private void OnTriggerEnter(Collider other)
    {

        go.SetActive(true);

    }

    void OnTriggerExit(Collider other)
    {

        go.SetActive(false);
    }
}
