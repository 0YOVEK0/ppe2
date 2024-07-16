using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 40;

    void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStats = other.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
