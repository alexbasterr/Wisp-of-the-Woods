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
    private void Update()
    {
        if (arbusto)
        {
            if (!noInteractuar)
            {
                if (Input.GetKeyDown(KeyCode.Space) && !moviendo && !dentroArbusto)
                {
                    GetComponent<CharacterController>().enabled = false;
                    dentroArbusto = true;
                    position = transform.position;
                    GetComponent<Animator>().SetTrigger("saltar");
                    moviendo = true;
                    noInteractuar = true;
                    
                    Vector3 lookPos = arbusto.transform.position - transform.position;
                    lookPos.y = 0;
                    transform.rotation = Quaternion.LookRotation(lookPos);
                }
                else if (Input.GetKeyDown(KeyCode.Space) && !moviendo && dentroArbusto)
                {
                    dentroArbusto = false;
                    GetComponent<Animator>().SetTrigger("saltar");
                    moviendo = true;
                    noInteractuar = true;
                    transform.eulerAngles = rotation - new Vector3(0, 180, 0);
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
            GetComponent<CharacterController>().enabled = true;
            timer = 0;
            position = Vector3.zero;
        }
    }

    public void ArbustoShakeEntrar()
    {
        if(dentroArbusto)
        {
            arbusto.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Activar");
            print("a");
        }
    }
    public void ArbustoShakeSalir()
    {
        if (!dentroArbusto)
        {
            arbusto.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Activar");
            print("b");
        }
    }

}
