using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class estabilizadorCamara : MonoBehaviour
{
    public Transform parent;
    public float rotacionX; 
    
    private void FixedUpdate()
    {
        rotacionX = parent.localEulerAngles.x + transform.parent.localEulerAngles.x;
        
        transform.localEulerAngles = new Vector3(-rotacionX, 0, 0);

    }
}
