using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeteccionSonido : MonoBehaviour
{
    bool puedeOir;
    public bool haOido;
    public Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = FindObjectOfType<PlayerMovement>().transform;
            player.GetComponent<AullarPlayer>().puedeAullar = true;
            player.GetComponent<AullarPlayer>().enemigo = this;
            puedeOir = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            puedeOir = false;
            player.GetComponent<AullarPlayer>().puedeAullar = false;
            player.GetComponent<AullarPlayer>().enemigo = null;
            player = null;
        }
    }
}
