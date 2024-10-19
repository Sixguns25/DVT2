using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControladorPuntos : MonoBehaviour
{
    public static ControladorPuntos Instance;
    [SerializeField] private int puntajeActual;
    [SerializeField] private int puntajeMaximo;
    public event EventHandler<SumarPuntosEventArgs> sumarPuntosEvnt;

    public class SumarPuntosEventArgs: EventArgs
    {
        public int puntajeActualEvnt;
        public int puntajeMaximoEvnt;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        puntajeMaximo = PlayerPrefs.GetInt("PuntajeMaximo");
    }

    public void SumarPuntos(int puntos)
    {
        puntajeActual += puntos;
        if (puntajeActual > puntajeMaximo) { 
            puntajeMaximo = puntajeActual;
        }

        sumarPuntosEvnt?.Invoke(this, new SumarPuntosEventArgs { puntajeActualEvnt = puntajeActual, puntajeMaximoEvnt = puntajeMaximo });

    }
}
