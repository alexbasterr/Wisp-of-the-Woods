using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeteccionProximidad : MonoBehaviour
{
    public bool playerEnRango;
    public GameObject player;
    public bool visto;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!other.GetComponent<EsconderPlayer>().dentroArbusto && !other.GetComponent<PlayerMovement>().detectado)
            {
                if (visto)
                    return;

                player = other.gameObject;
                playerEnRango = true;
                player.GetComponent<PlayerMovement>().detectado = true;
                player.GetComponent<PlayerMovement>().CanMove();
                visto = true;
            }
        }
    }
}
