using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texto_emergente : MonoBehaviour
{
    public GameObject go;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(gameObject.tag == "Aullar")
            {
                other.GetComponent<characterMovement>().aullar = true;
            }
            if(gameObject.tag == "Arbusto")
            {
                other.GetComponent<characterMovement>().interactuarArbustos = true;
                other.GetComponent<characterMovement>().arbusto = this.transform.parent.gameObject;
            }
            go.SetActive(true);

        }

    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            other.GetComponent<characterMovement>().aullar = false;
            other.GetComponent<characterMovement>().interactuarArbustos = false;
            go.GetComponent<activarAnim>().DesactivarAnim();
        }
    }

    public void desactivar()
    {
        go.GetComponent<activarAnim>().DesactivarAnim();
    }
}
