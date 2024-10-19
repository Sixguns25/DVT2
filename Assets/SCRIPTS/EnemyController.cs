using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    public Transform player; //posicion en la que esta el jugador
    public float detectionRadius = 5.0f; //para que se mueva unicamente si esta cerca de el jugador
    public float speed = 2.0f; //velocidad con la cual se movera hacia el jugador
    public float fuerzaRebote = 3f;
    private Rigidbody2D rb; // para moverlo
    private Vector2 movement; //direccion hacia donde se va a mover (-1 <- y -> +1)
    private bool enMovimiento;
    private bool recibiendoDanio;
    private bool playerVivo;

    private Animator animator;

    // Evento para notificar al Spawner cuando este enemigo sea destruido
    public event Action OnEnemyDestroyed;

    void Start()
    {
        playerVivo = true;
        rb = GetComponent<Rigidbody2D>();
        //obtener el animator del enemigo
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (playerVivo) 
        {
            Movimiento();
        }
        

        animator.SetBool("enMovimiento", enMovimiento);
    }

    private void Movimiento()
    {
        //distaceToPlayer -> almacena la distancia que tiene el enemigo hacia el jugador
        //Distance -> recibe 2 vectores a y b la cual nos devolvera la distancia entre estas posiciones (posicion del enemigo y la posicion del jugador)
        float distaceToPlayer = Vector2.Distance(transform.position, player.position);

        //verificamos si la distancia del jugador es menor que el radio de deteccion que se creo en la parte superior
        if (distaceToPlayer < detectionRadius)
        {
            // se le resta posiciones entre el jugador y nosotros (esto dará valores - o + dependiendo en la direccion en la que el jugador se encuentre)
            Vector2 direction = (player.position - transform.position).normalized;
            //Para que gire el enemigo
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }


            //a esta variable movement se le dice que va a ser igual a un vector2 en el cual unicamente nos moveremos de izq o derecha por lo tanto se le envia la direccion q se obtuvo en el eje x y el segundo eje 0
            movement = new Vector2(direction.x, 0);

            enMovimiento = true;
        }
        else
        {
            //si el jugador sale de la deteccion del enemigo entonces no deberá moverse
            movement = Vector2.zero;

            enMovimiento = false;
        }

        if (!recibiendoDanio)
        {
            //para mover al enemigo se debe usar su RigidBody y la funcion MovePosition la cual moverá el cuerpo hacía una direccion, como parámettro va a recibir unicamente un vector2 el cual va a ser una posicion q lo que hara sera mover a nuestro enemigo hacia cierta posicion..
            // para ello le enviamos el vector de nuestra posicion + la direccion del movimiento * la velocidad * time
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            Vector2 direccionDanio = new Vector2(transform.position.x, 0);
            Mov playerScript = collision.gameObject.GetComponent<Mov>();

            playerScript.RecibeDanio(direccionDanio, 1);
            playerVivo = !playerScript.muerto;
            if (!playerVivo) 
            {
                enMovimiento = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Espada"))
        {
            Vector2 direccionDanio = new Vector2(collision.gameObject.transform.position.x, 0);

            RecibeDanio(direccionDanio, 1);
        }
    }

    public void RecibeDanio(Vector2 direccion, int cantDanio)
    {
        //PARAMETROS: direccion de la cual provenga el daño y cuanto daño le causa el eneimgo
        if (!recibiendoDanio) //para que entre solo cuando el personaje no esté recibiendo daño
        {
            recibiendoDanio = true;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.1f).normalized; //un vector2 el cual va a restar la posicion del pj - la direcc a la q fuimos atacados (para q nos dirija a una direccion opuesta a la que el pj ha sido atacado), en el eje 'y'=1 para q de un salto 
            rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse); //añadir el rebote a modo de impulso

            StartCoroutine(DesactivaDanio());
        }


    }

    IEnumerator DesactivaDanio()
    {
        yield return new WaitForSeconds(0.4f);
        recibiendoDanio = false;
        rb.velocity = Vector2.zero;
    }

    //para saber hasta donde llegue su rango
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    void OnDestroy()
    {
        // Llamar al evento cuando el enemigo sea destruido
        if (OnEnemyDestroyed != null)
        {
            OnEnemyDestroyed();
        }
    }

}
