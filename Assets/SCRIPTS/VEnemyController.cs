using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VEnemyController : MonoBehaviour
{
    public Transform player;
    public float detectionRadius=5.0f;
    public float Speed=2.0f;


    private Rigidbody2D rb;
    private Vector2 movement;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Encuentra el objeto llamado "Goku" y asigna su Transform
        GameObject gokuObject = GameObject.Find("Goku");
        if (gokuObject != null)
        {
            player = gokuObject.transform;
        }
        else
        {
            Debug.LogError("No se encontró un objeto llamado 'Goku' en la escena.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distanceTopPlayer=Vector2.Distance(transform.position, player.position);

        if (distanceTopPlayer < detectionRadius) 
        { 
            Vector2 direction=(player.position-transform.position).normalized;
            movement=new Vector2(direction.x,0);
        }
        else
        {
            movement=Vector2.zero;
        }

        rb.MovePosition(rb.position + movement*Speed*Time.deltaTime);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Goku"))
    //    {
    //        Vector2 direccionamiento=new Vector2(transform.position.x,0);
    //        collision.gameObject.GetComponent<ProtagonistaController>().RecibeDanio(direccionamiento,1);
    //    }
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Goku"))
    //    {
    //        Vector2 direccionamiento = new Vector2(transform.position.x, 0); // Dirección del enemigo
    //        collision.gameObject.GetComponent<ProtagonistaController>().RecibeDanio(direccionamiento, 1);
    //    }
    //}


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
