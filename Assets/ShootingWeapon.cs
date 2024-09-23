using UnityEngine;

public class ShootingWeapon : MonoBehaviour
{
    public WeaponType weaponType;
    public Transform firePoint;

    public ResourceComponent resourceComponent; // Reference to the ResourceComponent

    private float cooldownTimer = 0f;
    private bool canShoot = true;

    void Start()
    {
        if (resourceComponent == null)
        {
            resourceComponent = GetComponent<ResourceComponent>();
        }
    }

    void Update()
    {
        HandleCooldown();
    }

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

    public void Shoot()
    {
        if (!canShoot) return; // Prevent shooting during cooldown

        // Check if there's enough resource (ammo/health) to shoot
        if (resourceComponent.UseResource(weaponType.resourceCost))
        {
            FireProjectiles();
            canShoot = false;
            cooldownTimer = weaponType.shootCooldown;
        }
        else
        {
            Debug.Log("Not enough resource (ammo/health) to shoot!");
        }
    }

    private void FireProjectiles()
    {
        if (weaponType.projectilePrefab == null)
        {
            Debug.LogWarning("ProjectilePrefab is missing.");
            return;
        }

        ShootInSpread();
    }

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

    private float GetSpreadAngle(int projectileIndex)
    {
        float halfSpread = weaponType.spreadAngle / 2f;
        float step = weaponType.spreadAngle / Mathf.Max(weaponType.projectilesPerShot - 1, 1);
        return -halfSpread + (step * projectileIndex);
    }
}
