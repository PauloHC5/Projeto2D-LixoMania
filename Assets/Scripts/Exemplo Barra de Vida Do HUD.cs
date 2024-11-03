using UnityEngine;

public class OscillateXScale : MonoBehaviour
{
    // Adjustable scale variables
    public float scaleSpeed = 1f;       // Speed of the scaling effect
    public float minXScale = 0.5f;      // Minimum X scale
    public float maxXScale = 1.5f;      // Maximum X scale
    public bool startScalingUp = true;  // Set initial scaling direction

    private bool scalingUp;

    void Start()
    {
        // Set the initial scaling direction based on the public bool
        scalingUp = startScalingUp;
    }

    void Update()
    {
        // Get the current X scale of the object
        float currentScaleX = transform.localScale.x;

        // Determine the new scale value based on the scaling direction
        if (scalingUp)
        {
            currentScaleX += scaleSpeed * Time.deltaTime;
            if (currentScaleX >= maxXScale)
            {
                currentScaleX = maxXScale;
                scalingUp = false; // Reverse direction
            }
        }
        else
        {
            currentScaleX -= scaleSpeed * Time.deltaTime;
            if (currentScaleX <= minXScale)
            {
                currentScaleX = minXScale;
                scalingUp = true; // Reverse direction
            }
        }

        // Apply the new scale to the object
        transform.localScale = new Vector3(currentScaleX, transform.localScale.y, transform.localScale.z);
    }
}