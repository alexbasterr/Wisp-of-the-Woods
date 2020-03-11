using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esconderse : MonoBehaviour
{
    public bool entradaOn;
        
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag  == "Player")
        {
            entradaOn = true;      
            other.gameObject.GetComponent<Player>().arbusto = this.transform;      
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            entradaOn = false;
            other.gameObject.GetComponent<Player>().arbusto = null;      
        }
    }

    public void ActivarAnimacion()
    {
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Activar");
    }
}
