using UnityEngine;

public class Cactus : MonoBehaviour
{
    public int damageAmount = 20;

    void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStats = other.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.TakeDamage(damageAmount);
        }
    }
}
