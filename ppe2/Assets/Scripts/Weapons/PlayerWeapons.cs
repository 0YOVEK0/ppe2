using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;

    void Start()
    {
        
        weapon1.SetActive(false);
        weapon2.SetActive(false);
        weapon3.SetActive(false);
    }

    public void ActivateWeapon(string weaponName)
    {
        switch (weaponName)
        {
            case "Weapon1":
                weapon1.SetActive(true);
                break;
            case "Weapon2":
                weapon2.SetActive(true);
                break;
            case "Weapon3":
                weapon3.SetActive(true);
                break;
            default:
                Debug.LogWarning("Weapon not found: " + weaponName);
                break;
        }
    }
}
