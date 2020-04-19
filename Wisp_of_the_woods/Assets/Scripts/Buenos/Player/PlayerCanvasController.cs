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
    }

    void Update()
    {
        print(Input.GetButtonDown("Menu"));

        if (Input.GetButtonDown("Menu"))
            OnMenu();
        if(anim.gameObject.GetComponent<Image>().color.a == 1)
        {

            if (goToCheckPoint)
            {
                LoadSceneMenu(1);
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
        menuActivo = false;
        menuPausa.SetActive(menuActivo);
        Time.timeScale = 1;
        movement.enabled = true;
    }

    public void ReloadCheckPoint()
    {
        Time.timeScale = 1;
        goToCheckPoint = true;
        anim.SetBool("On", true);
    }

    public void Options()
    {

    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        goToMenu = true;
        anim.SetBool("On", true);
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
