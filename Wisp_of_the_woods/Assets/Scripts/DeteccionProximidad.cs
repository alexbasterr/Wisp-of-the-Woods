using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeteccionProximidad : MonoBehaviour
{
    public bool playerEnRango;
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!other.GetComponent<EsconderPlayer>().dentroArbusto)
            {
                print("a");
                player = other.gameObject;
                playerEnRango = true;
                player.GetComponent<PlayerMovement>().detectado = true;
                player.GetComponent<PlayerMovement>().CanMove();
            }
        }
    }
}
