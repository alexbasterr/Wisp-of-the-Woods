using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeteccionSonido : MonoBehaviour
{
    bool puedeOir;
    public bool haOido;
    public Transform player;
    private void Update()
    {
        if(puedeOir && Input.GetKeyDown(KeyCode.F))
            haOido = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = FindObjectOfType<Player>().transform;
            puedeOir = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            puedeOir = false;
            player = null;
        }
    }
}
