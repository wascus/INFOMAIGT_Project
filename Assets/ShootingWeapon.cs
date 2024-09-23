using UnityEngine;

public class ShootingWeapon : MonoBehaviour
{
    public WeaponType weaponType;      // Weapon stats like ammo, fire rate, projectile, etc.

    public Transform firePoint;          // The point from where the projectile will be fired

    public int maxAmmo;                 // Maximum ammo for the weapon
    public int startingAmmo = 20;
    private int currentAmmo;             // Current ammo count
    private float cooldownTimer = 0f;    // Cooldown timer for shooting
    private bool canShoot = true;        // Flag to track if the player can shoot

    // Properties to expose ammo count
    public int MaxAmmo
    {
        get => maxAmmo;
        set => maxAmmo = Mathf.Max(value, 0); // Ensure max ammo can't be set to a negative value
    }

    public int CurrentAmmo => currentAmmo; // Read-only property for current ammo count

    void Start()
    {
        currentAmmo = startingAmmo;
    }

    void Update()
    {
        HandleCooldown();
    }

    // Handles cooldown logic, allowing the player to shoot again after a delay
    private void HandleCooldown()
    {
        if (!canShoot)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                canShoot = true; // Cooldown is over, can shoot again
            }
        }
    }

    // Method to handle shooting
    public void Shoot()
    {
        if (!canShoot) return; // Prevent shooting during cooldown

        if (currentAmmo > 0) // Only shoot if ammo is available
        {
            FireProjectiles();
            currentAmmo-=weaponType.ammoCost; // Decrease ammo count
            canShoot = false; // Set cooldown
            cooldownTimer = weaponType.shootCooldown; // Reset cooldown timer
        }
        else
        {
            Debug.Log("Out of Ammo!");

        }
    }

    private void FireProjectiles()
    {
        if (weaponType.projectilePrefab == null)
        {
            Debug.LogWarning("ProjectilePrefab is missing.");
            return; // Prevent errors if the projectile prefab is not set
        }
            ShootInSpread();

    }


    // Shoots a spread of projectiles from a single fire point
    private void ShootInSpread()
    {
        for (int i = 0; i < weaponType.projectilesPerShot; i++)
        {
            float angle = GetSpreadAngle(i);
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, angle);

            GameObject projectile = Instantiate(weaponType.projectilePrefab, firePoint.position, rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.AddForce(rotation * Vector3.up * weaponType.fireForce, ForceMode2D.Impulse);
            }
        }
    }

    // Calculate the spread angle for each projectile
    private float GetSpreadAngle(int projectileIndex)
    {
        float halfSpread = weaponType.spreadAngle / 2f;
        float step = weaponType.spreadAngle / Mathf.Max(weaponType.projectilesPerShot - 1, 1);
        return -halfSpread + (step * projectileIndex);
    }

    // Increases ammo up to the max ammo limit
    public void IncreaseAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo); // Prevent exceeding max ammo
        currentAmmo = Mathf.Max(currentAmmo, 0);
    }

    // Adjusts the maximum ammo (e.g., when picking up upgrades)
    public void AdjustMaxAmmo(int amount)
    {
        MaxAmmo += amount; // Adjust the max ammo while ensuring it can't go negative
        currentAmmo = Mathf.Min(currentAmmo, maxAmmo); // Adjust current ammo if max ammo is reduced
    }
}
