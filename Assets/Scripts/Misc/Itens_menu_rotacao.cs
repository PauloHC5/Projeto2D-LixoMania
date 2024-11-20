using UnityEngine;

public class DancingFallingObject : MonoBehaviour
{
    // Adjustable rotation variables
    public float rotationSpeed = 2f;
    public float rotationAngle = 10f;

    // Adjustable falling variables
    public float fallSpeed = 2f;
    public float yTeleportTrigger = -10f;
    public float yTeleportTo = 10f;

    private float startRotationZ;

    void Start()
    {
        // Store the initial rotation of the object
        startRotationZ = transform.eulerAngles.z;
    }

    void Update()
    {
        // Handle rotation
        float rotationZ = startRotationZ + Mathf.Sin(Time.time * rotationSpeed) * rotationAngle;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotationZ);

        // Handle falling
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        // Teleport to top if it reaches the trigger point
        if (transform.position.y <= yTeleportTrigger)
        {
            transform.position = new Vector3(transform.position.x, yTeleportTo, transform.position.z);
        }
    }
}