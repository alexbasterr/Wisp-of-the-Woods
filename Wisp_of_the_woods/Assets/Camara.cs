using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public float Ysensitivity;
    public float Xsensitivity;
    private float rotationY = 0f;
    private float rotationX = 0f;

    void OnEnable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        rotationY += Input.GetAxis("Mouse Y") * Ysensitivity;
        rotationX += Input.GetAxis("Mouse X") * Xsensitivity;

        rotationY = Mathf.Clamp(rotationY, -30, 0);
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, transform.localEulerAngles.z);

    }
}
