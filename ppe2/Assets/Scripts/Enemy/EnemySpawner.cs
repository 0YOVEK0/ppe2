using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Prefabs de enemigos
    public string playerTag = "Player"; // Etiqueta del jugador para identificarlo
    public int maxEnemies = 10; // Número máximo de enemigos a generar
    public Collider spawnArea; // Área del trigger para generar enemigos

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
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        int randEnemy = Random.Range(0, enemyPrefabs.Length);

        Vector3 randomPosition = GetRandomPositionInArea();

        GameObject newEnemy = Instantiate(enemyPrefabs[randEnemy], randomPosition, Quaternion.identity);

        // Asegurarse de que el enemigo tenga un Rigidbody con gravedad habilitada
        Rigidbody rb = newEnemy.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = newEnemy.AddComponent<Rigidbody>();
        }
        rb.useGravity = true;

        spawnedEnemies.Add(newEnemy);
    }

    Vector3 GetRandomPositionInArea()
    {
        Bounds bounds = spawnArea.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            transform.position.y,
            Random.Range(bounds.min.z, bounds.max.z)
        );
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
