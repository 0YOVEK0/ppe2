using UnityEngine;

public class DummyEnemy : MonoBehaviour
{
    public int health = 50;

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Dummy Enemy Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    // Método para destruir el dummy enemigo
    void Die()
    {
        Debug.Log("Dummy Enemy Died!");
        Destroy(gameObject);
    }
}
