using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour
{
    public Transform jugador;

    public Vector3 distanciaJugador;
    public float zoomActual;
    public float alturaMirarJugador;
    public float rotacionActualHorizontal;
    public float rotacionActualVertical;
    public float velocidadRotacionHorizontal;
    public float velocidadRotacionVertical;

    public characterMovement characterMovement;

    

    // Update is called once per frame
    void Update()
    {
        if (!characterMovement.menuPausa)
        {
            rotacionActualHorizontal += Input.GetAxis("Mouse X") * velocidadRotacionHorizontal * Time.deltaTime;
            rotacionActualVertical += Input.GetAxis("Mouse Y") * velocidadRotacionVertical * Time.deltaTime;
            rotacionActualVertical = Mathf.Clamp(rotacionActualVertical, -0.35f, 0);

            distanciaJugador = new Vector3(distanciaJugador.x, rotacionActualVertical, distanciaJugador.z);
        }
        
    }
    private void LateUpdate()
    {
        transform.position = jugador.position - (distanciaJugador * zoomActual); //marcar la distancia entre camara y jugador
        transform.LookAt(jugador.position + Vector3.up * alturaMirarJugador);// siempre mira al jugador
        transform.RotateAround(jugador.position, Vector3.up, rotacionActualHorizontal);// siempre rota alrededor del jugador

    }
}
