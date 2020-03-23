using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Velocity;
    [Space]

    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    public bool blockRotationPlayer;
    public float desiredRotationSpeed = 0.1f;
    public Animator anim;
    public float Speed;
    public float allowPlayerRotation = 0.1f;
    public Camera cam;
    public CharacterController controller;
    public bool isGrounded;

    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    public float verticalVel;
    private Vector3 moveVector;


    [Header("Detectado")]
    //Detectado
    public bool detectado;
    public Transform Enemy;

    [Header("Orientar")]
    //Orientar
    public LayerMask layer;
    Transform player;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();
        player = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        InputMagnitude();

        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            verticalVel -= 0;
        }
        else
        {
            verticalVel -= 1;
        }
        moveVector = new Vector3(0, verticalVel * .2f * Time.deltaTime, 0);
        controller.Move(moveVector);


    }

    private void FixedUpdate()
    {
        if (canMove())
        {
            AllignPlayer();
        }
    }

    public void AllignPlayer()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, layer))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(player.up, hit.normal) * player.rotation;
            player.rotation = Quaternion.Slerp(player.rotation, targetRotation, 50 * Time.deltaTime);
        }
        player.localEulerAngles = new Vector3(player.localEulerAngles.x, 0, player.localEulerAngles.z);
    }

    void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;

        if (blockRotationPlayer == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
            controller.Move(desiredMoveDirection * Time.deltaTime * Velocity);
        }
    }

    void InputMagnitude()
    {
        //Calculate Input Vectors
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        //Calculate the Input Magnitude
        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        //Physically move player
        if (canMove())
        {
            if (Speed > allowPlayerRotation)
            {
                anim.SetFloat("Mov", Speed, StartAnimTime, Time.deltaTime);
                PlayerMoveAndRotation();
            }
            else if (Speed < allowPlayerRotation)
            {
                anim.SetFloat("Mov", Speed, StopAnimTime, Time.deltaTime);
            }
        }
    }

    bool canMove()
    {
        if (detectado || GetComponent<EsconderPlayer>().dentroArbusto)
            return false;
        else
            return true;
    }
}
