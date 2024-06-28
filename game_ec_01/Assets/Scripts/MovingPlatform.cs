using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2.0f; // Speed of the platform movement
    public float leftLimit = -5.0f; // Left boundary relative to the initial position
    public float rightLimit = 5.0f; // Right boundary relative to the initial position

    private bool movingRight = true; // Direction of movement
    private float initialPositionX; // Initial X position of the platform

    void Start()
    {
        // Store the initial position of the platform
        initialPositionX = transform.position.x;
    }

    void Update()
    {
        // Calculate the new position
        Vector2 newPosition = transform.position;

        if (movingRight)
        {
            newPosition.x += speed * Time.deltaTime;
            if (newPosition.x >= initialPositionX + rightLimit)
            {
                newPosition.x = initialPositionX + rightLimit;
                movingRight = false;
            }
        }
        else
        {
            newPosition.x -= speed * Time.deltaTime;
            if (newPosition.x <= initialPositionX + leftLimit)
            {
                newPosition.x = initialPositionX + leftLimit;
                movingRight = true;
            }
        }

        // Apply the new position
        transform.position = newPosition;
    }
}

