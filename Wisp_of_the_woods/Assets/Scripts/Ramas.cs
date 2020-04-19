using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramas : MonoBehaviour
{
    public List<DeteccionSonido> detectores = new List<DeteccionSonido>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(DeteccionSonido ds in detectores)
            {
                ds.haOido = true;
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Cientifico"))
        {
            detectores.Add(other.gameObject.GetComponentInChildren<DeteccionSonido>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Cientifico"))
        {
            DeteccionSonido detector= other.gameObject.GetComponentInChildren<DeteccionSonido>();
            if (detectores.Contains(detector))
            {
                detectores.Remove(detector);
            }
        }
    }
}