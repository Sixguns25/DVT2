using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntaje : MonoBehaviour
{
    private TextMeshProUGUI TextMeshProUGUI;

    private void Start()
    {
        TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        //TextMeshProUGUI.text = PlayerPrefs.GetInt("PuntajeMaximo").ToString();
        ControladorPuntos.Instance.sumarPuntosEvnt += CambiarTexto;
    }

    public void CambiarTexto(object sender, ControladorPuntos.SumarPuntosEventArgs e)
    {
        TextMeshProUGUI.text = e.puntajeActualEvnt.ToString();
    }



}
