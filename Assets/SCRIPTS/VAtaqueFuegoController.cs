using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VAtaqueFuegoController : MonoBehaviour
{
    public GameObject bolaEnergiaPrefab;

    public float tiempoGeneracion = 3f;  // Tiempo en segundos entre cada generación
    private float contadorTiempo;

    

    // Start is called before the first frame update
    void Start()
    {
        contadorTiempo = tiempoGeneracion;  // Inicia el contador
        
    }

    // Update is called once per frame
    void Update()
    {
        contadorTiempo -= Time.deltaTime;  // Reducimos el contador según el tiempo transcurrido

        if (contadorTiempo <= 0f)
        {
            //audioSource.PlayOneShot(lanzarFuego);
            GameObject fuego = Instantiate(bolaEnergiaPrefab, transform.position, Quaternion.identity);
            contadorTiempo = tiempoGeneracion;  // Reiniciamos el contador
        }
    }
}
