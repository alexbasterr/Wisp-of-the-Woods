using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrinicpalManager : MonoBehaviour
{
    public Animator anim;

    public GameObject controles;

    private void Awake()
    {
        controles.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (anim.gameObject.GetComponent<Image>().color.a == 1)
            PlayOn();

        if (controles.activeInHierarchy && Input.GetButtonDown("Aullar"))
            controles.SetActive(false);
    }
    public void Play()
    {
        if (!controles.activeInHierarchy)
            anim.SetTrigger("On");
    }

    public void PlayOn()
    {
        SceneManager.LoadScene(1);
    }

    public void Controles()
    {
        controles.SetActive(true);
    }

    public void Exit()
    {
        if (!controles.activeInHierarchy)
            Application.Quit();
    }
}
