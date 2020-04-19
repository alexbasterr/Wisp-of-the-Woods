using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrinicpalManager : MonoBehaviour
{
    public Animator anim;
    public void Play()
    {
        anim.SetTrigger("On");
    }

    private void Update()
    {
        if (anim.gameObject.GetComponent<Image>().color.a == 1)
            PlayOn();
    }

    public void PlayOn()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
