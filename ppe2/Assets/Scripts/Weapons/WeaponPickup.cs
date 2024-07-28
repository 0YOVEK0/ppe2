using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public string weaponName; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            Transform weapon = other.transform.Find(weaponName);
            if (weapon != null)
            {
                weapon.gameObject.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
