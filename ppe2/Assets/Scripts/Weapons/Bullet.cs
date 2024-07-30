using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public float homingRadius = 50f; // Radio de búsqueda de enemigos
    public float homingStrength = 5f; // Fuerza de la corrección de dirección
    public float lifetime = 5f; // Tiempo de vida de la bala

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Destruir la bala después de cierto tiempo
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Buscar el enemigo más cercano
        GameObject target = FindClosestEnemy();
        if (target != null)
        {
            // Calcular la dirección hacia el enemigo
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Vector3 homingDirection = Vector3.Lerp(transform.forward, direction, Time.deltaTime * homingStrength).normalized;
            rb.velocity = homingDirection * speed;
        }
        else
        {
            // Si no hay enemigos, seguir en la dirección inicial
            rb.velocity = transform.forward * speed;
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= homingRadius)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    void OnTriggerEnter(Collider other)
    {
        // Si el proyectil golpea un objeto con la etiqueta "Enemy"
        if (other.CompareTag("Enemy"))
        {
            EnemyFollow enemy = other.GetComponent<EnemyFollow>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Destruir el proyectil
            Destroy(gameObject);
        }
    }
}
