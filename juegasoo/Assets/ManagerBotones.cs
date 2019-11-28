using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerBotones : MonoBehaviour
{

    private void OnLevelWasLoaded(int level)
    {
        if (menuPausa != null)
            menuPausa.SetActive(false);
    }

    public GameObject menuPausa;

    public void activarGameObject(GameObject objeto)
    {
        objeto.SetActive(!objeto.activeInHierarchy);
    }

    public void cargarPartida()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if (Time.timeScale == 0 && menuPausa != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            menuPausa.SetActive(true);
        }
        else if(Time.timeScale == 1 && menuPausa != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            menuPausa.SetActive(false);
        }

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Resume(characterMovement player)
    {
        player.menuPausa = false;
        Time.timeScale = 1;
    }

    public void irMenu()
    {
        menuPausa.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
