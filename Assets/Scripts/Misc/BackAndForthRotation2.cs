using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForthRotation2 : MonoBehaviour
{
    public float rotationSpeed = 1f;  // Speed of the back and forth rotation
    public float maxRotationAngle = 3f;  // Maximum angle for the rotation

    private float startingAngle;
    private float randomPhaseOffset;  // Random phase offset to desynchronize the rotation

    void Start()
    {
        // Store the initial Z rotation of the object
        startingAngle = transform.rotation.eulerAngles.z;

        // Generate a random phase offset for each object (in radians)
        randomPhaseOffset = Random.Range(0f, Mathf.PI * 2);  // Full sine wave cycle
    }

    void Update()
    {
        // Calculate the new rotation angle using a sine wave with a random phase offset
        float rotationAngle = maxRotationAngle * Mathf.Sin(Time.time * rotationSpeed + randomPhaseOffset);

        // Apply the rotation back and forth, only affecting the Z-axis
        transform.rotation = Quaternion.Euler(0, 0, startingAngle + rotationAngle);
    }
}
