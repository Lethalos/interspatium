using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 100f;
    public float jumpForce = 5f;
    public float crouchHeight = 0.5f;
    public float sprintSpeed = 10f;

    public float moveSpeed = 5f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        MoveCharacter(speed);
        RotateCharacter(sensitivity);
        JumpCharacter(jumpForce);
        CrouchCharacter(crouchHeight);
        SprintCharacter(sprintSpeed);
    }

    // move the character with WASD
    public void MoveCharacter(float speed)
    {
        // Get input from the WASD keys
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveVertical = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVertical = -1f;
        }
        
        

        // Create a Vector3 based on the input
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Move the player by setting the velocity of the Rigidbody
        rb.velocity = movement * moveSpeed;

    }

    // rotate the character with the mouse
    public void RotateCharacter(float sensitivity)
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);
    }

    // jump the character
    public void JumpCharacter(float jumpForce)
    {
        // jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // crouch the character
    public void CrouchCharacter(float crouchHeight)
    {
        // crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(1, crouchHeight, 1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // sprint the character
    public void SprintCharacter(float sprintSpeed)
    {
        // sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            MoveCharacter(sprintSpeed);
        }
    }



}
