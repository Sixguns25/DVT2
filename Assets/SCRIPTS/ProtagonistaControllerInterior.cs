using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistaController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    private SpriteRenderer sr;

    public float fuerzaSalto = 10f;
    public float longitudRaycast = 0.1f;
    public LayerMask capaSuelo;

    private bool enSuelo;
    private bool rebotando; // Variable de estado para el rebote temporal

    //SONIDOS
    //Salto
    public AudioClip jumpSound;
    public AudioClip golpeSound;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rebotando = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!rebotando)
        {
            animator.SetInteger("Estado", 0);
            rb.velocity = new Vector2(0, rb.velocity.y);

            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(25, rb.velocity.y);
                sr.flipX = false;
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-25, rb.velocity.y);
                sr.flipX = true;
            }

            if (rb.velocity.x != 0)
            {
                animator.SetInteger("Estado", 1);
            }


            if (Input.GetKey(KeyCode.J) && enSuelo)
            {
                if (sr.flipX)
                {
                    rb.velocity = new Vector2(-10, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(10, rb.velocity.y);
                }
                

                animator.SetInteger("Estado", 3);
            }
            if (Input.GetKeyDown(KeyCode.J) && enSuelo)
                audioSource.PlayOneShot(golpeSound);

            if (Input.GetKey(KeyCode.U)&& enSuelo)
            {
                animator.SetInteger("Estado", 4);
                
            }


        }

        if (rebotando)
        {
            animator.SetInteger("Estado",5);
        }
           

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
        enSuelo = hit.collider != null;

        if (enSuelo && Input.GetKeyDown(KeyCode.K))
        {
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            animator.SetInteger("Estado", 2);
            audioSource.PlayOneShot(jumpSound);
        }
        if (!enSuelo && !rebotando)
        {
            animator.SetInteger("Estado", 2);
            //if (Input.GetKey(KeyCode.J))
            //{
            //    animator.SetInteger("Estado", 3);
            //}
        }
        


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaDinamica1")
        {
            transform.parent = collision.transform;
        }

        if (collision.gameObject.tag == "Enemigo" || collision.gameObject.tag == "EnemigoAgua" || collision.gameObject.tag == "EnemigoVolador" && rebotando==true)
        {
            float direction = transform.position.x - collision.transform.position.x;
            float fuerzaHorizontal = direction > 0 ? 12f : -12f;

            // Establece el estado de rebote y aplica la fuerza
            rebotando = true;
            rb.AddForce(new Vector2(fuerzaHorizontal, fuerzaSalto), ForceMode2D.Impulse);

            // Restaura el control horizontal despu s de un breve per odo
            Invoke(nameof(FinRebote), 0.8f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaDinamica1")
        {
            transform.parent = null;
        }
    }

    // Funci n para restaurar el control horizontal
    private void FinRebote()
    {
        rebotando = false;
    }

}
