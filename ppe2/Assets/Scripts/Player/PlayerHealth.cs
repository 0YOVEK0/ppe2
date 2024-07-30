using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthBar; // Referencia a la barra de salud en la UI
    private UIManager uiManager;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        uiManager = FindObjectOfType<UIManager>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");

        // Desactivar todos los scripts EnemyFollow en la escena
        EnemyFollow[] enemies = FindObjectsOfType<EnemyFollow>();
        foreach (EnemyFollow enemy in enemies)
        {
            enemy.Disable();
        }

        // Mostrar la pantalla de muerte
        if (uiManager != null)
        {
            uiManager.ShowDeathScreen();
        }

        // Desactivar el jugador en lugar de destruirlo
        gameObject.SetActive(false);
    }

    // Método para curar al jugador (opcional)
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log("Player healed for " + amount + " health. Current health: " + currentHealth);

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }
}
