using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    float defaultVelocityX = 15f;  // Velocidad predeterminada
    float currentVelocityX = 0f;   // Velocidad actual
    Rigidbody2D rb;                // Referencia al Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3);  // Destruye la bola de fuego después de 3 segundos
    }

    void Update()
    {
        // Mueve la bola de fuego en la dirección establecida
        rb.velocity = new Vector2(currentVelocityX, rb.velocity.y);
    }

    // Método para establecer la dirección de la bola de fuego
    public void SetDirection(string direction)
    {
        if (direction == "left")
        {
            // Si la dirección es izquierda, invertimos la velocidad en X
            currentVelocityX = -defaultVelocityX;

            // Rotamos la bola de fuego 180 grados en el eje Y para que apunte hacia la izquierda
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            // Si la dirección es derecha, mantenemos la velocidad positiva
            currentVelocityX = defaultVelocityX;

            // Aseguramos que la bola de fuego esté orientada correctamente hacia la derecha (sin rotación)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // Detectar colisiones
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            Destroy(collision.gameObject);  // Destruye al enemigo
            Destroy(gameObject);            // Destruye la bola de fuego
        }
    }
}
