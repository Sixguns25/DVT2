using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class VControladorDatosJuego : MonoBehaviour
{
    public GameObject jugador;
    public string archivoDeGuardado;
    public VDatosJuego datosJuego=new VDatosJuego();

    private void Awake()
    {
        archivoDeGuardado = Application.dataPath + "/datosJuego.Json";

        jugador=GameObject.FindGameObjectWithTag("Goku");

        //CargarDatos();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CargarDatos();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GuardarDatos();
        }
    }

    private void CargarDatos()
    {
        if (File.Exists(archivoDeGuardado))
        {
            string contenido = File.ReadAllText(archivoDeGuardado);
            datosJuego=JsonUtility.FromJson<VDatosJuego>(contenido);

            Debug.Log("Posicion Jugador: " + datosJuego.posicion);
            jugador.transform.position=datosJuego.posicion;
            jugador.GetComponent<VPerderVidas>().CantVidas=datosJuego.vidas;
            jugador.GetComponent<VAgarrarMoneda>().monedas=datosJuego.puntos;
        }
        else
        {
            Debug.Log("No hay datos guardados");
        }
    }

    private void GuardarDatos()
    {
        VDatosJuego nuevosDatos = new VDatosJuego()
        {
            posicion=jugador.transform.position,
            vidas=jugador.GetComponent<VPerderVidas>().CantVidas,
            puntos=jugador.GetComponent<VAgarrarMoneda>().monedas
        };

        string cadenaJSON=JsonUtility.ToJson(nuevosDatos);

        File.WriteAllText(archivoDeGuardado, cadenaJSON);

        Debug.Log("Archivo Guardado");
    }
}
