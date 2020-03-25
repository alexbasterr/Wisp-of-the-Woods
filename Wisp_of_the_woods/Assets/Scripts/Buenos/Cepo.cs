using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cepo : MonoBehaviour
{
    Animator anim;
    bool enRango;
    bool atrapado;
    Transform player;
    public float speedMultiplier;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (enRango)
        {
            anim.speed = speedMultiplier / Vector3.Distance(transform.position, player.position);
            if (Vector3.Distance(transform.position, player.position) <= 0.5f && !atrapado)
            {
                anim.SetFloat("Distance", 0,0,Time.deltaTime);
                player.GetComponent<PlayerMovement>().CanMove();
                atrapado = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetFloat("Distance", 0.5f);
            enRango = true;
            player = other.transform;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetFloat("Distance", 1);
            enRango = false;
            player = null;
        }
    }
}
