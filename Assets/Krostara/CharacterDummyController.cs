using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDummyController : MonoBehaviour
{
    public float speed = 10.0f; // Speed of the character

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // A and D or left and right arrow keys
        float moveVertical = Input.GetAxis("Vertical"); // W and S or up and down arrow keys

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
