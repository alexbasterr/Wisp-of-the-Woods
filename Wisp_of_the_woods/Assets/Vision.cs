using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public bool playerEnRango;
    public GameObject player;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            playerEnRango = true;
            player = other.transform.parent.gameObject;
            player.GetComponent<Player>().detectado = true;
            player.GetComponent<Player>().Enemy = transform.parent;
        }
    }
}
