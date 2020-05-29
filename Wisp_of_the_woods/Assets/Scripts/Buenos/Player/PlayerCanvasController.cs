using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerCanvasController : MonoBehaviour
{
    bool cinematicaHecha = false;

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

    public List<Transform> checkpoints = new List<Transform>();
    public List<Transform> objetivos = new List<Transform>();
    public Transform CheckpointParent;
    public int actualChekpoint;
    public GameObject camaraCinematica;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        menuActivo = false;
        menuPausa.SetActive(false);
        menuControles.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SetCheckpoints();

    }


    void SetCheckpoints()
    {
        int i = 0;

        foreach (Transform child in CheckpointParent)
        {
            checkpoints.Add(child);
            child.GetComponent<Checkpoint>().checkpointNumber = i;
            i++;
        }
    }

    void Update()
    {

        if(actualChekpoint == 1 && !cinematicaHecha)
        {
            cinematicaHecha = true;
            camaraCinematica.SetActive(true);
            movement.enabled = false;
            StartCoroutine(ActiveMovement());
        }

        if(actualChekpoint == 1)
        {
            objetivos[0].position = objetivos[1].position;
        }
        else if(actualChekpoint == 2)
        {
            objetivos[0].position = objetivos[2].position;
            objetivos[1].position = objetivos[2].position;
        }
        else if (actualChekpoint == 3)
        {
            objetivos[0].position = objetivos[3].position;
            objetivos[1].position = objetivos[3].position;
            objetivos[2].position = objetivos[3].position;
        }
        else if (actualChekpoint == 4)
        {
            objetivos[0].position = objetivos[4].position;
            objetivos[1].position = objetivos[4].position;
            objetivos[2].position = objetivos[4].position;
            objetivos[3].position = objetivos[4].position;
        }

        for (int i = 0; i < objetivos.Count; i++)
        {
            if (i == actualChekpoint)
                objetivos[i].gameObject.SetActive(true);
            else
                objetivos[i].gameObject.SetActive(false);
        }

        if (Input.GetButtonDown("Menu"))
            OnMenu();
        if(anim.gameObject.GetComponent<Image>().color.a == 1)
        {

            if (goToCheckPoint)
            {
                GoToCheckpoint();
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

        if (Input.GetKeyDown(KeyCode.R))
            ReloadCheckPoint();
    }


    IEnumerator ActiveMovement()
    {
        yield return new WaitForSeconds(16.2f);
        movement.enabled = true;
        camaraCinematica.SetActive(false);
    }
    void GoToCheckpoint()
    {
        transform.position = checkpoints[actualChekpoint].position;
        transform.rotation = checkpoints[actualChekpoint].rotation;
        anim.SetBool("On", false);
        Resume();
    }

    void OnMenu()
    {
        menuActivo = true;
        menuPausa.SetActive(menuActivo);
        Time.timeScale = 0;
        movement.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
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

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
