using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texto_emergente : MonoBehaviour
{
    public GameObject go;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            go.SetActive(true);

    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
            go.SetActive(false);
    }
}
