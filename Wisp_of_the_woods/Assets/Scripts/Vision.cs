using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public bool playerEnRango;
    public GameObject player;
    public bool visto;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !other.GetComponent<EsconderPlayer>().dentroArbusto && !other.GetComponent<PlayerMovement>().detectado)
        {
            if (visto)
                return;

            playerEnRango = true;
            player = other.gameObject;
            player.GetComponent<PlayerMovement>().detectado = true;
            player.GetComponent<PlayerMovement>().CanMove();
            player.GetComponent<PlayerMovement>().Enemy = transform.parent;
            visto = true;
        }
    }
}
