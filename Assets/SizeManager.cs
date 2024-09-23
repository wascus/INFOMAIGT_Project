using UnityEngine;

public class SizeManager : MonoBehaviour
{
    public ResourceComponent resourceComponent; // Reference to the ResourceComponent
    public Transform playerTransform;      // Reference to the player's transform
    public Vector3 minSize = new Vector3(0.5f, 0.5f, 1f); // Minimum size when resource is 0
    public Vector3 maxSize = new Vector3(1.5f, 1.5f, 1f); // Maximum size when resource is full
    public float smoothTime = 0.5f; // Smoothing time for scale changes

    private Vector3 currentVelocity = Vector3.zero; // Used for smooth damping

    void Start()
    {
      playerTransform.localScale = GetTargetSize();
    }

    private Vector3 GetTargetSize()
    {
      int currentResource = resourceComponent.CurrentResource;
      int maximumResource = resourceComponent.MaximumResource;

      // Calculate the size ratio based on resource count
      float sizeRatio = (float)currentResource / maximumResource;

      // Lerp between minSize and maxSize based on sizeRatio
      Vector3 targetSize = Vector3.Lerp(minSize, maxSize, sizeRatio);
      return targetSize;
    }

    void Update()
    {
        // Lerp between minSize and maxSize based on sizeRatio
        Vector3 targetSize = GetTargetSize();

        // Smoothly interpolate the size over time
        playerTransform.localScale = Vector3.SmoothDamp(playerTransform.localScale, targetSize, ref currentVelocity, smoothTime);
    }
}
