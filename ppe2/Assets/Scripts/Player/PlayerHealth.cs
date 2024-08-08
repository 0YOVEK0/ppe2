using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxShield = 50; // Máxima energía del escudo
    private int currentHealth;
    private int currentShield;
    public Slider healthBar; // Referencia a la barra de salud en la UI
    public Slider shieldBar; // Referencia a la barra de energía en la UI
    private UIManager uiManager;

    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (shieldBar != null)
        {
            shieldBar.maxValue = maxShield;
            shieldBar.value = currentShield;
        }

        uiManager = FindObjectOfType<UIManager>();
    }

    public void TakeDamage(int damage)
    {
        // Primero, aplica el daño al escudo si está disponible
        if (currentShield > 0)
        {
            int shieldDamage = Mathf.Min(damage, currentShield);
            currentShield -= shieldDamage;
            damage -= shieldDamage;

            if (shieldBar != null)
            {
                shieldBar.value = currentShield;
            }
        }

        // Si queda daño después de aplicar al escudo, aplica el daño a la salud
        if (damage > 0)
        {
            currentHealth -= damage;
            Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);

            if (healthBar != null)
            {
                healthBar.value = currentHealth;
            }
        }

        // Si el escudo está lleno, el daño restante se transfiere de nuevo al escudo
        if (currentShield <= 0 && damage > 0)
        {
            int shieldFill = Mathf.Min(damage, maxShield - currentShield);
            currentShield += shieldFill;
            damage -= shieldFill;

            if (shieldBar != null)
            {
                shieldBar.value = currentShield;
            }
        }

        // Comprueba si la salud del jugador ha llegado a cero
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
        if (currentShield < maxShield)
        {
            int shieldHeal = Mathf.Min(amount, maxShield - currentShield);
            currentShield += shieldHeal;
            amount -= shieldHeal;

            if (shieldBar != null)
            {
                shieldBar.value = currentShield;
            }
        }

        if (amount > 0)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
            Debug.Log("Player healed for " + amount + " health. Current health: " + currentHealth);

            if (healthBar != null)
            {
                healthBar.value = currentHealth;
            }
        }
    }
}
