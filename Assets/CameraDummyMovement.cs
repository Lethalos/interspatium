using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDummyMovement : MonoBehaviour
{
    public Transform player; // The player transform to follow
    public Vector3 offset; // The offset distance between the player and camera

    void Start()
    {
        // If offset is not set, calculate it based on initial positions
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        // Update the camera position to follow the player with the offset
        transform.position = player.position + offset;
    }
}
