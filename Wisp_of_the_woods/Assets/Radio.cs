using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    GameObject player;
    Animator anim;
    public float distancia;
    public float disntanciaMinima;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(player != null)
        {
            distancia = Vector3.Distance(transform.position, player.transform.position);
            anim.SetFloat("Distance", distancia);
        }
        if (distancia < disntanciaMinima)
            ActivarTrampa();

    }

    public void ActivarTrampa()
    {
        print("Trampa activada");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            player = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            player = null;
    }
}
