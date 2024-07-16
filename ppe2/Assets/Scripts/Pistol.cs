using UnityEngine;

public class Pistol : Weapon
{
    public GameObject bulletPrefab;  // Prefab del proyectil
    public Transform firePoint;      // Punto de disparo

    void Start()
    {
        fireRate = 1.5f;
        damage = 10;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    public override void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage = damage;
    }
}
