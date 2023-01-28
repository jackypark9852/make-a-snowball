using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Test script used for testing the snowball animation. 
// Not for use in the final game.
public class SnowballController : MonoBehaviour
{
    public float moveSpeed = 10.0f; // The speed at which the object will move
    private Vector3 direction = Vector3.forward; // The direction in which the object will move

    void Update()
    {
        // Check for input in the horizontal axis (W and S keys)
        float horizontal = Input.GetAxis("Horizontal");
        // Check for input in the vertical axis (A and D keys)
        float vertical = Input.GetAxis("Vertical");

        // Update the direction vector based on the input values
        direction = new Vector3(horizontal, 0, vertical);

        // Normalize the direction vector to ensure consistent movement speed
        direction = direction.normalized;

        // Move the object in the direction specified by the direction variable, multiplied by the moveSpeed and the time passed since the last frame
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}