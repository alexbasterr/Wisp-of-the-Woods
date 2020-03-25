using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbusto : MonoBehaviour
{
    bool entradaOn;
        
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag  == "Player")
        {
            entradaOn = true;      
            other.gameObject.GetComponent<EsconderPlayer>().arbusto = gameObject;
            other.gameObject.GetComponent<PlayerCanvasController>().ActivarSalto();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            entradaOn = false;
            other.gameObject.GetComponent<EsconderPlayer>().arbusto = null;
            other.gameObject.GetComponent<PlayerCanvasController>().DesactivarSalto();
        }
    }

    public void ActivarAnimacion()
    {
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Activar");
    }
}
