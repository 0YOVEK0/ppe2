using UnityEngine;

public class HealthPill : MonoBehaviour
{
    public int healthRecoveryPercentage = 40;

    void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStats = other.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            int healthToRecover = playerStats.maxHealth * healthRecoveryPercentage / 100;
            playerStats.Heal(healthToRecover);
            Destroy(gameObject);
        }
    }
}
