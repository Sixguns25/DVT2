using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] private int cantidadPuntos;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            ControladorPuntos.Instance.SumarPuntos(cantidadPuntos);
            Destroy(gameObject);
        }
    }
}
