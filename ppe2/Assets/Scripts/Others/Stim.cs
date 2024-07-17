using UnityEngine;

public class Stim : MonoBehaviour
{
    public int energyRecoveryPercentage = 40;

    void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStats = other.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            int energyToRecover = playerStats.maxEnergy * energyRecoveryPercentage / 100;
            playerStats.RechargeEnergy(energyToRecover);
            Destroy(gameObject);
        }
    }
}
