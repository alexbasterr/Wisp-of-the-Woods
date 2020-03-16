using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvasController : MonoBehaviour
{

    public GameObject AullarCanvas;
    public GameObject SaltoCanvas;

    public void ActivarAullar()
    {
        AullarCanvas.SetActive(true);
    }

    public void ActivarSalto()
    {
        SaltoCanvas.SetActive(true);

    }
    public void DesactivarAullar()
    {
        AullarCanvas.SetActive(false);
    }

    public void DesactivarSalto()
    {
        SaltoCanvas.SetActive(false);
    }
}
