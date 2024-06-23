using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public Vector3 offset;    // Offset position between the player and the camera
    public float smoothSpeed = 0.125f;  // Speed at which the camera will smooth towards the player

    void Start()
    {
        // Initialize the offset based on the initial positions of the player and the camera
        if (player != null)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        // Only follow the player if the player is assigned
        if (player != null)
        {
            // Calculate the desired position with the offset
            Vector3 desiredPosition = player.position + offset;

            // Smoothly interpolate between the camera's current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera's position
            transform.position = smoothedPosition;

            // Optionally, keep the camera looking at the player
            // transform.LookAt(player);
        }
    }
}
