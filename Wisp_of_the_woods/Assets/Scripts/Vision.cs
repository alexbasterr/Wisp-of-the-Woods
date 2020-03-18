using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public bool playerEnRango;
    public GameObject player;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && !other.GetComponent<EsconderPlayer>().dentroArbusto)
        {
            playerEnRango = true;
            player = other.gameObject;
            player.GetComponent<PlayerMovement>().detectado = true;
            player.GetComponent<PlayerMovement>().Enemy = transform.parent;
        }
    }
}
