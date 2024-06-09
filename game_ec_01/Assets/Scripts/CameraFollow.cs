using UnityEngine;

public class CameraFollow : MonoBehaviour {

    //Gameobject of the player
    public Transform target; 

    //Offset for camera
    public Vector3 offset; 

    //Camera speed
    public float smoothSpeed = 0.125f; 

    private void FixedUpdate() {

        if (target != null) {
            // Calculate wanted position of the camera
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update camera position
            transform.position = smoothedPosition;
        }
    }
}
