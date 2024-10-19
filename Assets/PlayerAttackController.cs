using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Animator animator; // Añade referencia al Animator

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Obtén el componente Animator del personaje
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            // Activa el bool para lanzar la habilidad
            animator.SetBool("lanzandoHabilidad", true);

            // Iniciar la corutina que espera la duración de la animación para lanzar la bola de fuego
            StartCoroutine(LanzarHabilidadDespuesDeAnimacion());
        }
    }

    IEnumerator LanzarHabilidadDespuesDeAnimacion()
    {
        yield return new WaitForSeconds(1.4f); // Duración de la animación

        // Instanciar la bola de fuego y pasar la dirección según el personaje esté volteado o no
        GameObject FireBall = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

        // Pasamos la dirección según si el personaje está mirando a la izquierda o la derecha
        FireBall.GetComponent<FireBallController>().SetDirection(sr.flipX ? "left" : "right");

        // Desactiva el bool después de lanzar la bola de fuego
        animator.SetBool("lanzandoHabilidad", false);
    }

}
