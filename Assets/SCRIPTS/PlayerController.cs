using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov : MonoBehaviour
{
    public float velocidad = 5f;
    public int vida = 10;

    public float fuerzaSalto = 10f;
    public float fuerzaRebote = 5f;
    public float longitudRaycast = 0.1f; //la línea roja
    public LayerMask capaSuelo;

    private bool enSuelo;
    private bool recibiendoDanio; //para activar y desactivar la animacion de golpeado
    private bool atacando;
    public bool muerto;

    private Rigidbody2D rb;

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!muerto) {
            if (!atacando)
            {
                Movimiento();

                // Comprobar si está en el suelo (//posición del jugador, mirar hacia abajo, va a ser tmñ de la long raycast que se asignó, busca colisionar con la capa suelo)
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
                //si la line acolisiona con el suelo sería verdadero, en cambio si la linea no esta colisionando con el suelo eso va a ser falso
                enSuelo = hit.collider != null;

                // Salto
                if (enSuelo && Input.GetKeyDown(KeyCode.Space) && !recibiendoDanio)
                {
                    //al rb se le añade una fuerza que va a ser igual al vector2 que en x va a ser 0 y en 'y' se pone la fuerza del salto
                    rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
                }
            }
            if (Input.GetKeyDown(KeyCode.F) && !atacando && enSuelo)
            {
                Atacando();
            }
        }
        Animaciones();
    }

    public void Movimiento()
    {
        // Movimiento horizontal
        float velocidadX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;

        // Control de animación (si usas animaciones)
        animator.SetFloat("movement", velocidadX * velocidad);

        // Cambiar dirección según la tecla presionada
        if (velocidadX < 0)
        {
            // Mira a la izquierda
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (velocidadX > 0)
        {
            // Mira a la derecha
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Movimiento del personaje (izquierda/derecha)
        Vector3 posicion = transform.position;

        if (!recibiendoDanio) //para que el pj cuando reciba daño no se pueda mover
        {
            transform.position = new Vector3(velocidadX + posicion.x, posicion.y, posicion.z);
        }
    }
    public void Animaciones()
    {
        animator.SetBool("ensuelo", enSuelo);
        animator.SetBool("recibeDanio", recibiendoDanio);
        animator.SetBool("Atacando", atacando);
        animator.SetBool("muerto", muerto);
    }

    //función para que se ejecute solo cuando el enemigo colisione con mi personaje
    public void RecibeDanio(Vector2 direccion, int cantDanio)
    {
        //PARAMETROS: direccion de la cual provenga el daño y cuanto daño le causa el eneimgo
        if (!recibiendoDanio) //para que entre solo cuando el personaje no esté recibiendo daño
        {
            recibiendoDanio = true;
            vida -= cantDanio;
            if (vida <= 0) 
            {
                muerto = true;
            }
            //cuando este muerto no debe haber rebote
            else
            {
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.1f).normalized; //un vector2 el cual va a restar la posicion del pj - la direcc a la q fuimos atacados (para q nos dirija a una direccion opuesta a la que el pj ha sido atacado), en el eje 'y'=1 para q de un salto 
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse); //añadir el rebote a modo de impulso
            }
            

            
        }
        
        
    }

    //función para que desactive el recibiendo daño
    public void DesactivaDanio()
    {
        recibiendoDanio = false;
        rb.velocity = Vector2.zero;
    }

    public void Atacando()
    {
        atacando = true;
    }
    public void DesactivaAtaque()
    {
        atacando = false;
    }

    void OnDrawGizmos() //figura imaginarias que unicamente se van a ver en el editor más no en el juego
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
                        //De donde nace el vecor - hacia donde va
    }

}


