using UnityEngine;

public class SizeManager : MonoBehaviour
{
    public ShootingWeapon shootingWeapon;  // Reference to the ShootingWeapon script
    public Transform playerTransform;      // Reference to the player's transform
    public Vector3 minSize = new Vector3(0.5f, 0.5f, 1f); // Minimum size when ammo is 0
    public Vector3 maxSize = new Vector3(1.5f, 1.5f, 1f); // Maximum size when ammo is full

    void Update()
    {
        // Get the current ammo and max ammo from ShootingWeapon
        int currentAmmo = shootingWeapon.CurrentAmmo;
        int maxAmmo = shootingWeapon.maxAmmo;

        // Calculate the size ratio based on ammo count
        float sizeRatio = (float)currentAmmo / maxAmmo;

        // Lerp between minSize and maxSize based on sizeRatio
        Vector3 newSize = Vector3.Lerp(minSize, maxSize, sizeRatio);

        // Apply the new size to the player
        playerTransform.localScale = newSize;
    }
}
