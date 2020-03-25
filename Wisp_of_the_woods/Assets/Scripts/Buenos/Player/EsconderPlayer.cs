using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsconderPlayer : MonoBehaviour
{
    public AnimationCurve curve;
    public GameObject arbusto;
    public bool dentroArbusto;

    public bool noInteractuar;
    float timer;
    public bool moviendo;

    public Vector3 position;
    Vector3 rotation;
    BoxCollider arbustoCollider;
    private void Update()
    {
        if (arbusto)
        {
            if (!noInteractuar)
            {
                if (Input.GetButtonDown("Jump") && !moviendo && !dentroArbusto)
                {
                    GetComponent<PlayerMovement>().CanMove();
                    arbustoCollider = arbusto.GetComponent<BoxCollider>();
                    arbustoCollider.enabled = false;
                    dentroArbusto = true;
                    position = transform.position;
                    GetComponent<Animator>().SetTrigger("saltar");
                    moviendo = true;
                    noInteractuar = true;
                    
                    Vector3 lookPos = arbusto.transform.position - transform.position;
                    lookPos.y = 0;
                    transform.rotation = Quaternion.LookRotation(lookPos);
                }
                else if (Input.GetButtonDown("Jump") && !moviendo && dentroArbusto)
                {
                    dentroArbusto = false;
                    position = arbusto.transform.GetChild(1).GetChild(0).position + transform.forward;

                    Vector3 lookPos = position - transform.position;
                    lookPos.y = 0;
                    transform.rotation = Quaternion.LookRotation(lookPos);

                    GetComponent<Animator>().SetTrigger("saltar");
                    moviendo = true;
                    noInteractuar = true;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (moviendo)
        {
            if (dentroArbusto)
                EntrarArbusto();
            
            if(!dentroArbusto)
                SalirArbusto();
        }

        if(arbusto && dentroArbusto)
        {
            arbusto.transform.GetChild(1).rotation = Quaternion.LookRotation(Camera.main.transform.forward, arbusto.transform.up);
            arbusto.transform.GetChild(1).localEulerAngles = new Vector3(0, arbusto.transform.GetChild(1).localEulerAngles.y, 0);
        }
    }

    public void EntrarArbusto()
    {
        timer += Time.deltaTime;
        if (timer >= 4)
            timer = 4;

        rotation = transform.eulerAngles;

        transform.position = Vector3.Lerp(transform.position, arbusto.transform.position + (Vector3.up *0.22f), curve.Evaluate(timer/4));

    }
    public void SalirArbusto()
    {
        transform.GetChild(0).gameObject.SetActive(true);

        timer += Time.deltaTime;
        if (timer >= 4)
            timer = 4;

        transform.position = Vector3.Lerp(transform.position, position, curve.Evaluate(timer / 4));
    }
    public void FinAnimacion()
    {
        if (dentroArbusto)
        {
            moviendo = false;
            noInteractuar = false;
            transform.GetChild(0).gameObject.SetActive(false);
            timer = 0;
        }
        else
        {
            moviendo = false;
            noInteractuar = false;
            arbustoCollider.enabled = true;
            timer = 0;
            position = Vector3.zero;
            GetComponent<PlayerMovement>().CanMove();
        }
    }

    public void ArbustoShakeEntrar()
    {
        if(dentroArbusto)
        {
            arbusto.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Activar");
        }
    }
    public void ArbustoShakeSalir()
    {
        if (!dentroArbusto)
        {
            arbusto.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Activar");
        }
    }

}
