using UnityEngine;

public class MoveUpDownRelativeToParent : MonoBehaviour
{
    // Adjustable movement variables
    public float moveSpeed = 2f;       // Speed of the movement
    public float lowerYOffset = -1f;   // Offset from the parent's position for the lower Y point
    public float upperYOffset = 1f;    // Offset from the parent's position for the upper Y point

    private bool movingUp = true;      // Track the movement direction
    private float parentYPosition;     // Store the parent's Y position

    void Start()
    {
        // Initialize the parent's Y position
        parentYPosition = transform.parent.position.y;
    }

    void Update()
    {
        // Calculate the target Y positions relative to the parent
        float lowerYPosition = parentYPosition + lowerYOffset;
        float upperYPosition = parentYPosition + upperYOffset;

        // Get the current Y position of the object
        float currentY = transform.position.y;

        // Determine the new position based on the movement direction
        if (movingUp)
        {
            currentY += moveSpeed * Time.deltaTime;
            if (currentY >= upperYPosition)
            {
                currentY = upperYPosition;
                movingUp = false; // Reverse direction
            }
        }
        else
        {
            currentY -= moveSpeed * Time.deltaTime;
            if (currentY <= lowerYPosition)
            {
                currentY = lowerYPosition;
                movingUp = true; // Reverse direction
            }
        }

        // Apply the new position relative to the parent's position
        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
    }
}
