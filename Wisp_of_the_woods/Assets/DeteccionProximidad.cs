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
            player = FindObjectOfType<Player>().gameObject;
            player.GetComponent<Player>().detectado = true;
            player.GetComponent<Player>().Enemy = transform.parent;
        }
    }
}
