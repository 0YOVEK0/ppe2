using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public Rigidbody rb;

    void Start()
    {
        rb.velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        // Si el proyectil golpea un objeto con la etiqueta "Enemy"
        if (other.CompareTag("Enemy"))
        {
            THC6_ctrl enemy = other.GetComponent<THC6_ctrl>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Destruir el proyectil
            Destroy(gameObject);
        }
    }
}
