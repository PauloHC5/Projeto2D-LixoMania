using UnityEngine;

public class BackAndForthRotation : MonoBehaviour
{
    public float rotationSpeed = 0.5f;  // Speed of the back and forth rotation
    public float maxRotationAngle = 3f;  // Maximum angle for the rotation

    private float startingAngle;

    void Start()
    {
        // Store the initial Z rotation of the object
        startingAngle = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        // Calculate the new rotation angle using a sine wave
        float rotationAngle = maxRotationAngle * Mathf.Sin(Time.time * rotationSpeed);

        // Apply the rotation back and forth, only affecting the Z-axis
        transform.rotation = Quaternion.Euler(0, 0, startingAngle + rotationAngle);
    }
}