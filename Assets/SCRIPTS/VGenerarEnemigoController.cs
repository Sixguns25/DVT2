//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GenerarEnemigoController : MonoBehaviour
//{
//    public GameObject enemigoPrefab;

//    public float tiempoGeneracion = 5f;  // Tiempo en segundos entre cada generación
//    private float contadorTiempo;
//    // Start is called before the first frame update
//    void Start()
//    {
//        contadorTiempo = tiempoGeneracion;  // Inicia el contador
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        contadorTiempo -= Time.deltaTime;  // Reducimos el contador según el tiempo transcurrido

//        if (contadorTiempo <= 0f)
//        {
//            //audioSource.PlayOneShot(lanzarFuego);
//            GameObject fuego = Instantiate(enemigoPrefab, transform.position, Quaternion.identity);
//            contadorTiempo = tiempoGeneracion;  // Reiniciamos el contador
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VGenerarEnemigoController : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public float tiempoGeneracion = 5f;  // Tiempo en segundos entre cada generación
    private float contadorTiempo;
    public int limiteEnemigos = 5;  // Límite de enemigos en escena
    public float rangoGeneracionX = 10f;  // Rango de unidades para mover a izquierda o derecha

    private List<GameObject> enemigosActivos = new List<GameObject>();  // Lista para los enemigos activos

    // Start is called before the first frame update
    void Start()
    {
        contadorTiempo = tiempoGeneracion;  // Inicia el contador
    }

    // Update is called once per frame
    void Update()
    {
        // Solo generar si el número de enemigos activos es menor que el límite
        if (enemigosActivos.Count < limiteEnemigos)
        {
            contadorTiempo -= Time.deltaTime;  // Reducimos el contador según el tiempo transcurrido

            if (contadorTiempo <= 0f)
            {
                // Generamos una posición aleatoria dentro del rango especificado en el eje X
                Vector3 posicionGeneracion = transform.position;
                posicionGeneracion.x += Random.Range(-rangoGeneracionX, rangoGeneracionX);

                // Instanciamos el enemigo y lo añadimos a la lista de enemigos activos
                GameObject nuevoEnemigo = Instantiate(enemigoPrefab, posicionGeneracion, Quaternion.identity);
                enemigosActivos.Add(nuevoEnemigo);

                // Reiniciamos el contador de tiempo
                contadorTiempo = tiempoGeneracion;
            }
        }

        // Limpiar la lista de enemigos destruidos
        enemigosActivos.RemoveAll(enemigo => enemigo == null);  // Elimina enemigos destruidos (null)
    }
}
