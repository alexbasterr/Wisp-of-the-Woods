using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public GameObject camaraPlayer;
    public GameObject CMCamera;


    private void Awake()
    {
        playerMovement.enabled = false;
        CMCamera.SetActive(false);
        camaraPlayer.SetActive(false);
    }

    void DesactivarPlayer()
    {
    }

    void FinAnimacion()
    {
        playerMovement.enabled = true;
        CMCamera.SetActive(true);
        camaraPlayer.SetActive(true);
        gameObject.SetActive(false);
    }
}
