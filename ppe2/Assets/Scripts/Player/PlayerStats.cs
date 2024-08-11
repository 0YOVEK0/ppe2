using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int maxEnergy = 100;
    public int currentEnergy;

    public HealthBar healthBar;
    public EnergyBar energyBar;
    public GameObject deathCanvas; // Referencia al Canvas de muerte

    private bool isEnergyArmorActive = false;
    private int energyConsumptionPerSecond = 10; // Consumo de energía por segundo para EnergyArmor

    void Start()
    {
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;

        healthBar.SetMaxHealth(maxHealth);
        energyBar.SetMaxEnergy(maxEnergy);

        if (deathCanvas != null)
        {
            deathCanvas.SetActive(false); // Asegúrate de que el Canvas de muerte esté desactivado al inicio
        }
    }

    void Update()
    {
        // Simulación de recibir daño (puedes eliminar esto una vez tengas proyectiles implementados)
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }

        // Simulación de gastar energía (puedes eliminar esto una vez tengas habilidades implementadas)
        if (Input.GetKeyDown(KeyCode.Y))
        {
            UseEnergy(10);
        }

        // Consumir energía por segundo cuando la EnergyArmor esté activa
        if (isEnergyArmorActive)
        {
            ConsumeEnergy(energyConsumptionPerSecond);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void UseEnergy(int amount)
    {
        currentEnergy -= amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        energyBar.SetEnergy(currentEnergy);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    public void RechargeEnergy(int amount)
    {
        currentEnergy += amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        energyBar.SetEnergy(currentEnergy);
    }

    public void ConsumeEnergy(int amount)
    {
        currentEnergy -= amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        energyBar.SetEnergy(currentEnergy);
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    public void RemoveHealth(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    public void ActivateEnergyArmor()
    {
        if (currentEnergy >= energyConsumptionPerSecond)
        {
            isEnergyArmorActive = true;
            Debug.Log("Energy Armor Activated");
        }
        else
        {
            Debug.Log("Not enough energy to activate Energy Armor.");
        }
    }

    public void DeactivateEnergyArmor()
    {
        isEnergyArmorActive = false;
        Debug.Log("Energy Armor Deactivated");
    }

    private void Die()
    {
        Debug.Log("Player Died");

        // Desactivar el jugador
        gameObject.SetActive(false);

        // Mostrar el Canvas de muerte
        if (deathCanvas != null)
        {
            deathCanvas.SetActive(true);
        }
    }
}
