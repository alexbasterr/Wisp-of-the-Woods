using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbusto : MonoBehaviour
{
    public bool entradaOn;
        
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag  == "Player")
        {
            entradaOn = true;      
            other.gameObject.GetComponent<EsconderPlayer>().arbusto = gameObject;      
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            entradaOn = false;
            other.gameObject.GetComponent<EsconderPlayer>().arbusto = null;      
        }
    }

    public void ActivarAnimacion()
    {
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Activar");
    }
}
