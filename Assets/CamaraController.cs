using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform objetivo; //para nuestro hero
    public float velocidadCamara = 0.025f;
    public Vector3 desplazamiento;

    //esta función se ejecuta después de update, se ejecuta despues del movimiento, ya q el mov del jugador está en el update
    private void LateUpdate()
    {
        Vector3 posicionDeseada = objetivo.position + desplazamiento;
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadCamara);
        // la funcion lerp es basicamente la interpolacion entre 2 posiciones(posicion de la camara y posicion a la q se desea mover, con la velocidadCamara)
        
        //la posicion de la camara va a ser igual a la posicion suavizada
        transform.position = posicionSuavizada;
    }
}
