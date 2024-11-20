using UnityEngine;

public class ObjectVibration2D : MonoBehaviour
{
    // Public variables to control vibration intensity and speed (these should show up in the Inspector)
    public float vibrationAmount = 0.07f; // How far the object vibrates from the original position
    public float vibrationSpeed = 30f;   // How fast the object vibrates

    // Store the original position of the object
    private Vector2 originalPosition;

    void Start()
    {
        // Store the initial position of the object when the game starts
        originalPosition = transform.position;
    }

    void Update()
    {
        // Create random offsets within the vibration amount (only in X and Y since it's a 2D game)
        float offsetX = Random.Range(-vibrationAmount, vibrationAmount);
        float offsetY = Random.Range(-vibrationAmount, vibrationAmount);

        // Calculate the new position by adding the offset to the original position
        Vector2 vibrationOffset = new Vector2(offsetX, offsetY);

        // Apply the vibration effect by moving the object to the new position
        transform.position = Vector2.Lerp(transform.position, originalPosition + vibrationOffset, Time.deltaTime * vibrationSpeed);
    }
}
