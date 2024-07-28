using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public HealthBar healthBar;

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Aqu� puedes agregar la l�gica para lo que sucede cuando el jugador muere
        Debug.Log("Player died!");
        // Por ejemplo, puedes desactivar el objeto del jugador
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificamos si el objeto con el que colisionamos tiene la etiqueta "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Suponiendo que el da�o del enemigo es 10, puedes cambiarlo seg�n tus necesidades
            TakeDamage(10);
        }
    }
}
