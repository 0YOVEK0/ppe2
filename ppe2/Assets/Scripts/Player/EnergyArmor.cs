using UnityEngine;

public class EnergyArmor : MonoBehaviour
{
    public int energyShield = 50; // Cantidad de escudo que proporciona la armadura
    private bool isActive = false;

    private PlayerStats playerStats;
    private int energyConsumptionPerSecond = 10;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats component not found on the player.");
        }
    }

    void Activate()
    {
        if (playerStats.currentEnergy >= energyConsumptionPerSecond)
        {
            isActive = true;
            Debug.Log("Energy Armor Activated");
        }
        else
        {
            Debug.Log("Not enough energy to activate Energy Armor.");
        }
    }

    void Deactivate()
    {
        isActive = false;
        Debug.Log("Energy Armor Deactivated");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isActive)
            {
                Deactivate();
            }
            else
            {
                Activate();
            }
        }

        if (isActive)
        {
            // Consumir energía por segundo
            playerStats.UseEnergy(energyConsumptionPerSecond);
        }
    }
}
