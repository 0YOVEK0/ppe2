using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSP : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public string playerTag = "Player"; // Etiqueta del jugador para identificarlo
    public int maxEnemies = 10; // Número máximo de enemigos a generar

    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private bool spawningActive = true;

    void Start()
    {
        // No es necesario hacer nada en Start en este caso
    }

    void Update()
    {
        if (spawningActive)
        {
            CheckEnemiesStatus();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (spawningActive && other.CompareTag(playerTag) && spawnedEnemies.Count < maxEnemies)
        {
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            GameObject newEnemy = Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
            spawnedEnemies.Add(newEnemy);
        }
    }

    void CheckEnemiesStatus()
    {
        spawnedEnemies.RemoveAll(enemy => enemy == null); // Elimina enemigos muertos de la lista

        if (spawnedEnemies.Count == 0 && spawnedEnemies.Count >= maxEnemies)
        {
            spawningActive = false; // Desactiva la generación de enemigos si todos han muerto
        }
    }
}
