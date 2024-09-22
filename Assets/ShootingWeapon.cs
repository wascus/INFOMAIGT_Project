using System.Collections;
using UnityEngine;

public class ShootingWeapon : MonoBehaviour
{
    public GameObject projectilePrefab;   // Prefab for the projectile to be shot
    public Transform firePoint;           // The point from where the projectile will be fired
    public float fireForce = 20f;         // The force with which the projectile is fired
    public float shootCooldown = 0.5f;    // Cooldown time between each shot
    public int maxAmmo = 10;              // Maximum ammo available

    private float cooldownTimer = 0f;     // Timer to track cooldown between shots
    private bool canShoot = true;         // Flag to track if the player can shoot
    private int currentAmmo;              // Current ammo count

    public int CurrentAmmo => currentAmmo; // Expose current ammo count to other scripts

    void Start()
    {
        currentAmmo = maxAmmo;            // Initialize ammo count
    }

    void Update()
    {
        // Handle cooldown timer
        if (!canShoot)
        {
            cooldownTimer -= Time.deltaTime;  // Reduce cooldown timer

            if (cooldownTimer <= 0f)
            {
                canShoot = true;             // Reset shooting ability after cooldown
            }
        }
    }

    public void Shoot()
    {
        if (canShoot && currentAmmo > 0)  // Only shoot if cooldown is over and ammo is available
        {
            // Instantiate projectile and apply force
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);

            // Reduce ammo count and reset cooldown
            currentAmmo--;
            canShoot = false;
            cooldownTimer = shootCooldown;
        }
        else if (currentAmmo <= 0)
        {
            Debug.Log("Out of Ammo!");  // Optional: Debug message for when out of ammo
        }
    }

    public void IncreaseAmmo(int ammo)
    {
        currentAmmo = Mathf.Min(currentAmmo + ammo, maxAmmo);  // Reload but don't exceed max ammo
    }

    public void ChangeMaxAmmo(int amount)
    {
      maxAmmo = maxAmmo + amount;
    }
}
