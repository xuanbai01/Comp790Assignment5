using UnityEngine;

public class LightsaberController : MonoBehaviour
{
    public GameObject blade; // Reference to the blade GameObject
    public float extendSpeed = 0.1f; // Speed for extension/collapse

    private bool weaponActive = true; // Lightsaber on/off state
    private float minScale = 0f; // Minimum y-scale for collapse
    private float maxScale; // Maximum y-scale for full extension
    private float extendDelta; // Scaling increment/decrement
    private float currentScale; // Current y-scale of the blade
    private float localScaleX, localScaleZ; // Original x and z scales for the blade

    void Start()
    {
        // Capture initial x and z scales
        localScaleX = blade.transform.localScale.x;
        localScaleZ = blade.transform.localScale.z;

        // Set maxScale to the blade's initial y scale
        maxScale = blade.transform.localScale.y;

        // Initialize currentScale to maxScale (fully extended)
        currentScale = maxScale;

        // Calculate the delta value for smooth scaling
        extendDelta = maxScale / extendSpeed;

        // Position the blade's base by setting its initial local position
        blade.transform.localPosition = new Vector3(0, currentScale / 2, 0);

        // Set the blade's initial scale
        blade.transform.localScale = new Vector3(localScaleX, currentScale, localScaleZ);
    }

    void Update()
    {
    // Check for spacebar input to toggle lightsaber on/off
        if (Input.GetKeyDown(KeyCode.Space))
        {
        // Toggle the extendDelta direction based on the current state
            extendDelta = weaponActive ? -Mathf.Abs(extendDelta) : Mathf.Abs(extendDelta);
        }

    // Adjust the current scale smoothly
        currentScale += extendDelta * Time.deltaTime;

    // Clamp the scale to prevent it from exceeding bounds
        currentScale = Mathf.Clamp(currentScale, minScale, maxScale);

    // Apply the calculated scale to the blade's y-axis and keep x and z constant
        blade.transform.localScale = new Vector3(localScaleX, currentScale, localScaleZ);

    // Adjust the blade's position so it extends/collapses from the base
        blade.transform.localPosition = new Vector3(0, currentScale / 2, 0);

    // Update weapon state based on whether the blade is extended
        weaponActive = currentScale > minScale;

    // Final logic to toggle the blade's visibility
        if (weaponActive && !blade.activeSelf)
        {
        // If the weapon is active and blade is not visible, activate it
            blade.SetActive(true);
        }
        else if (!weaponActive && blade.activeSelf)
        {
        // If the weapon is inactive and blade is visible, deactivate it
            blade.SetActive(false);
        }
    }

}
