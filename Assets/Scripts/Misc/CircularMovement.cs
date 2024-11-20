using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    // Public variable to define the diameter of the circle
    public float diameter = 0.2f;
    // Public variable to adjust the speed of rotation
    public float speed = 0.5f;

    // Internal variable to store the radius (half of the diameter)
    private float radius;

    // Starting position of the object
    private Vector2 startPosition;

    // Current angle of the object
    private float angle;

    void Start()
    {
        // Calculate the radius based on the diameter
        radius = diameter / 2f;

        // Store the initial position of the object
        startPosition = transform.position;

        // Set a random starting angle between 0 and 360 degrees
        angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
    }

    void Update()
    {
        // Increment the angle based on time and speed
        angle += speed * Time.deltaTime;

        // Calculate the new position using sine and cosine for circular motion
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        // Set the new position relative to the initial position (keeping z constant)
        transform.position = new Vector2(startPosition.x + x, startPosition.y + y);
    }
}