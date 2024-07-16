using UnityEngine;

public class Shotgun : Weapon
{
    public GameObject bulletPrefab;  // Prefab del proyectil
    public Transform firePoint;      // Punto de disparo
    public int pellets = 5;
    public float spreadDistance = 1f;  // Distancia entre los proyectiles

    void Start()
    {
        fireRate = 2f;
        damage = 5;
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
        for (int i = 0; i < pellets; i++)
        {
            // Calcular la posición de cada proyectil en una línea recta
            Vector3 offset = firePoint.right * (i - (pellets - 1) / 2f) * spreadDistance;
            Vector3 spawnPosition = firePoint.position + offset;
            Quaternion rotation = firePoint.rotation;
            
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.damage = damage;
        }
    }
}
