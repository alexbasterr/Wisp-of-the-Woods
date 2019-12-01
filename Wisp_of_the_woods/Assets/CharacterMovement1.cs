using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement1 : MonoBehaviour
{

    public float velocidad;
    
    void FixedUpdate()
    {

        if(Input.GetAxis("Vertical") > 0)
            transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * velocidad * Time.fixedDeltaTime,Space.Self);
        else if (Input.GetAxis("Horizontal") > 0)
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * velocidad * Time.fixedDeltaTime, Space.Self);
    }
}
