using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCanvasController : MonoBehaviour
{

    public GameObject AullarCanvas;
    public GameObject SaltoCanvas;
    public GameObject menuPausa;
    public GameObject menuControles;
    public Animator anim;
    PlayerMovement movement;
    public bool menuActivo;
    bool goToMenu;
    bool goToCheckPoint;
    [HideInInspector]
    public bool detectado;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        menuActivo = false;
        menuPausa.SetActive(false);
        menuControles.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        if (Input.GetButtonDown("Menu"))
            OnMenu();
        if(anim.gameObject.GetComponent<Image>().color.a == 1)
        {

            if (goToCheckPoint)
            {
                LoadSceneMenu(2);
            }
            else if (goToMenu)
            {
                LoadSceneMenu(0);
            }
        }

        if(detectado)
        {
            ReloadCheckPoint();
        }

        if (menuControles.activeInHierarchy && Input.GetButtonDown("Aullar"))
            menuControles.SetActive(false);
    }

    void OnMenu()
    {
        menuActivo = true;
        menuPausa.SetActive(menuActivo);
        Time.timeScale = 0;
        movement.enabled = false;
        
    }
        
    public void LoadSceneMenu(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Resume()
    {
        if (!menuControles.activeInHierarchy)
        {
            menuActivo = false;
            menuPausa.SetActive(menuActivo);
            Time.timeScale = 1;
            movement.enabled = true;
        }
    }

    public void ReloadCheckPoint()
    {
        if (!menuControles.activeInHierarchy)
        {
            Time.timeScale = 1;
            goToCheckPoint = true;
            anim.SetBool("On", true);
        }
    }

    public void Options()
    {
        menuControles.SetActive(true);
    }

    public void GoToMenu()
    {
        if (!menuControles.activeInHierarchy)
        {
            Time.timeScale = 1;
            goToMenu = true;
            anim.SetBool("On", true);
        }
    }



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
