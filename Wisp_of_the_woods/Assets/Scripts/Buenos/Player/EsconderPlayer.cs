using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsconderPlayer : MonoBehaviour
{
    public GameObject arbusto;

    public Transform camara;
    public Transform modelo;

    private Vector3 posicionModelo;
    private Vector3 posicionCamara;

    public bool dentroArbusto;

    private bool noInteractuar;

    private void Update()
    {
        if (arbusto)
        {
            if (!noInteractuar)
            {
                if (Input.GetKeyDown(KeyCode.Space) && dentroArbusto)
                {
                    noInteractuar = true;
                    SalirArbusto();
                }
                else if (Input.GetKeyDown(KeyCode.Space) && !dentroArbusto)
                {
                    noInteractuar = true;
                    EntrarArbusto();
                }
            }
        }
    }
    public void EntrarArbusto()
    {
        GetComponent<Animator>().SetTrigger("saltar");
        Vector3 euler = modelo.localEulerAngles;
        modelo.LookAt(arbusto.transform);
        modelo.localEulerAngles = new Vector3(euler.x, modelo.localEulerAngles.y, euler.z);
        posicionModelo = modelo.localPosition;
        posicionCamara = camara.localPosition;
        camara.position = arbusto.transform.position;
        modelo.GetComponent<CapsuleCollider>().enabled = false;
        StartCoroutine(CO_EntrarArbusto(2));
    }

    public void SalirArbusto()
    {
        GetComponent<Animator>().SetTrigger("saltar");
        modelo.localEulerAngles -= new Vector3(0, 180, 0);
        camara.GetComponent<Camara>().rotationX -= 180;
        camara.localPosition = posicionCamara;
        StartCoroutine(CO_SalirArbusto(1));
    }

    IEnumerator CO_EntrarArbusto(float tiempo)
    {
        Transform a = modelo;
        for (int i = 0; i <= 50; i++)
        {
            modelo.position = new Vector3(Mathf.Lerp(a.position.x, arbusto.transform.position.x, i * 0.02f), Mathf.Lerp(a.position.y, arbusto.transform.position.y + 1, i * 0.02f), Mathf.Lerp(a.position.z, arbusto.transform.position.z, i * 0.02f));
            yield return new WaitForSeconds(tiempo * 0.02f);
        }
        dentroArbusto = true;
        noInteractuar = false;
    }

    IEnumerator CO_SalirArbusto(float tiempo)
    {
        Vector3 a = modelo.localPosition;
        for (int i = 0; i <= 50; i++)
        {
            modelo.localPosition = new Vector3(Mathf.Lerp(a.x, posicionModelo.x, i * 0.02f), Mathf.Lerp(a.y, posicionModelo.y, i * 0.02f), Mathf.Lerp(a.z, posicionModelo.z, i * 0.02f));
            yield return new WaitForSeconds(tiempo * 0.02f);
        }
        dentroArbusto = false;
        noInteractuar = false;
        modelo.GetComponent<CapsuleCollider>().enabled = true;
    }
}
