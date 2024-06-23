using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDummyFollower : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset; // Offset distance between the player and camera

    private void Start()
    {
        // Initialize the offset value if not set in the inspector
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    private void LateUpdate()
    {
        // Update the position of the camera
        transform.position = player.position + offset;
    }
}
