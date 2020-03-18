using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeteccionProximidad : MonoBehaviour
{
    public bool playerEnRango;
    public GameObject player;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            playerEnRango = true;
            player = FindObjectOfType<PlayerMovement>().gameObject;
            player.GetComponent<PlayerMovement>().detectado = true;
            player.GetComponent<PlayerMovement>().Enemy = transform.parent;
        }
    }
}
