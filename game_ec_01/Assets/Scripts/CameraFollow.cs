using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player GameObject's Transform
    public Vector3 offset;   // Offset between the camera and the player
    public float smoothSpeed = 0.125f; // Speed of camera movement

    private void FixedUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position for the camera
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update camera position
            transform.position = smoothedPosition;
        }
    }
}
