using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Componentes
    Animator anim;

    //Movement
    public float maxSpeed;
    public Transform camara;
    private float speed;
    private Vector2 mov;

    //Detectado
    public bool detectado;
    public Transform Enemy;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        speed = maxSpeed;
    }

    private void Update()
    {
        if (canMove())
            mov = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        else
            mov = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (canMove() && mov.magnitude != 0)
            Movement();
    }

    private void LateUpdate()
    {
        UpdateAnimations();
    }

    void Movement()
    {
        transform.GetChild(1).transform.forward = camara.forward;
        transform.Translate(mov.y * speed * Time.deltaTime * camara.forward);
        transform.Translate(mov.x * speed * Time.deltaTime * camara.right);
    }

    bool canMove()
    {
        if (detectado || GetComponent<EsconderPlayer>().dentroArbusto)
            return false;
        else
            return true;
    }

    void UpdateAnimations()
    {
        if (mov.sqrMagnitude != 0)
            anim.SetBool("andar", true);
        else
            anim.SetBool("andar", false);
    }
    
}
