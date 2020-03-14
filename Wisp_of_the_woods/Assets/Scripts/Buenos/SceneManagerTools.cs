using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerTools : MonoBehaviour
{
    public GameObject[] Zona1;
    public GameObject[] Zona2;
    private void Awake()
    {
        SetGameObject(Zona1, true);
        SetGameObject(Zona2, false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SetGameObject(Zona1, true);
            SetGameObject(Zona2, false);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            SetGameObject(Zona1, false);
            SetGameObject(Zona2, true);
        }
    }


    void SetGameObject(GameObject[] array, bool valor)
    {
        foreach (GameObject objeto in array)
            objeto.SetActive(valor);
    }
}
