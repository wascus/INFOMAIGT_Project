using UnityEngine;

public class UpgradePickable : MonoBehaviour
{
    public enum UpgradeType { Health, Ammo, MaxAmmo } // Different types of upgrades
    public UpgradeType upgradeType;  // Choose the type in the Inspector
    public int value = 1;  // The value of the upgrade (e.g., +10 health or +1 ammo)
    private SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer component

    // Define colors for each upgrade type
    public Color healthColor = Color.red;   // Health upgrade color
    public Color ammoColor = Color.blue;    // Ammo upgrade color
    public Color maxAmmoColor = Color.green; // Weapon upgrade color

    void Start()
    {
        // Get the SpriteRenderer component attached to the same GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the color based on the upgrade type
        switch (upgradeType)
        {
            case UpgradeType.Health:
                spriteRenderer.color = healthColor;
                break;
            case UpgradeType.Ammo:
                spriteRenderer.color = ammoColor;
                break;
            case UpgradeType.MaxAmmo:
                spriteRenderer.color = maxAmmoColor;
                break;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player is the one colliding with the upgrade
        if (collision.gameObject.CompareTag("Player"))
        {
          ShootingWeapon shootingWeapon = collision.gameObject.GetComponent<ShootingWeapon>();
          HealthComponent health = collision.gameObject.GetComponent<HealthComponent>();
            {
                switch (upgradeType)
                {
                    case UpgradeType.Health:

                        health.Heal(value);
                        break;
                    case UpgradeType.Ammo:

                        shootingWeapon.IncreaseAmmo(value);
                        break;
                    case UpgradeType.MaxAmmo:
                        shootingWeapon.ChangeMaxAmmo(value);
                        break;
                }
            }

            // Destroy the upgrade object after being collected
            Destroy(gameObject);
        }
    }
}
