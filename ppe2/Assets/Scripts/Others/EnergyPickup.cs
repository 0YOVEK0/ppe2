using UnityEngine;

public class EnergyPickup : MonoBehaviour
{
    public int energyAmount = 40;

    void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStats = other.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.RechargeEnergy(energyAmount);
            Destroy(gameObject);
        }
    }
}
