using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esconderse : MonoBehaviour
{
    public bool entradaOn;
    
        
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag.Equals("Player"))
        {
            entradaOn = true;            
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == ("Player"))
        {
            entradaOn = false;
        }
    }

}
