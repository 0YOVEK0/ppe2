using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float fireRate;
    public int damage;

    protected float nextFireTime = 0f;

    public abstract void Shoot();
}
