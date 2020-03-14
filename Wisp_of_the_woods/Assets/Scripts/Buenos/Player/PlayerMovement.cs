using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Componentes
    Animator anim;

    //Movement
    public float maxSpeed;
    float speed;
    Vector2 mov;
    public Transform camara;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        speed = maxSpeed;
    }

    private void Update()
    {
        if (canMove())
            mov = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
