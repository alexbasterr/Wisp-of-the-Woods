using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AullarPlayer : MonoBehaviour
{
    public DeteccionSonido enemigo;

    public bool puedeAullar;
    PlayerCanvasController canvasPlayer;
    Animator anim;
    bool aullarRequest;


    private void Awake()
    {
        canvasPlayer = GetComponent<PlayerCanvasController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (puedeAullar && Input.GetButtonDown("Aullar") && !aullarRequest && !GetComponent<EsconderPlayer>().dentroArbusto && enemigo != null)
        {
            aullarRequest = true;
            Aullar();
        }
    }

    public void Aullar()
    {
        GetComponent<PlayerMovement>().CanMove();
        anim.SetTrigger("Aullar");
    }

    public void FinAullar()
    {
        GetComponent<PlayerMovement>().CanMove();
        aullarRequest = false;
        enemigo.haOido = true;
    }
}
