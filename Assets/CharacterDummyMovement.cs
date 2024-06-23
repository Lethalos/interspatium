using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDummyMovement : MonoBehaviour
{
    public float speed = 5.0f; // Movement speed
    public float gravity = -9.8f; // Gravity force
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        // Get the CharacterController component
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the character is grounded
        if (controller.isGrounded)
        {
            // Get input from the WASD keys
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Determine the movement direction based on input
            moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }

        // Apply gravity to the movement direction
        moveDirection.y += gravity * Time.deltaTime;

        // Move the character controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}
