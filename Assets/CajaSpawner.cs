using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaSpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo que ser� instanciado
    public Transform spawnPoint; // Punto donde aparecer�n los enemigos (la caja)
    public float spawnInterval = 3.0f; // Tiempo entre cada spawn de enemigo
    public int maxEnemies = 5; // M�ximo n�mero de enemigos activos en la escena
    private int currentEnemyCount = 0; // Contador de enemigos activos
    public Transform playerTransform; // Referencia al jugador

    void Start()
    {
        // Iniciar el ciclo de respawn
        StartCoroutine(SpawnEnemy());
    }

    // Corrutina para respawnear enemigos
    IEnumerator SpawnEnemy()
    {
        while (true) // Bucle infinito para seguir generando enemigos
        {
            if (currentEnemyCount < maxEnemies) // Si hay menos enemigos que el m�ximo permitido
            {
                // Instanciar el enemigo en la posici�n de la caja
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

                // Asignar la referencia del jugador al script EnemyController
                enemy.GetComponent<EnemyController>().player = playerTransform;

                // Incrementar el conteo de enemigos
                currentEnemyCount++;

                // Si deseas controlar que cada enemigo destruido libere espacio para m�s enemigos
                enemy.GetComponent<EnemyController>().OnEnemyDestroyed += EnemyDestroyed;
            }

            // Esperar el tiempo de intervalo antes de generar otro enemigo
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Este m�todo se llamar� cuando un enemigo sea destruido
    void EnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
