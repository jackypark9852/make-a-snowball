using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SnowballlLogic : MonoBehaviour
{
    [Tooltip("The layers that the snowball will fire on when it collides with them")]
    [SerializeField] private LayerMask _fireOnCollision;
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Fire(Vector3 initialVelocity)
    { // Called by the player when the snowball is fired
        GameObject anchorObject = transform.parent.gameObject; // Hardcoded to have anchor object as parent
        transform.parent = null; // Detach the snowball from the player
        Destroy(anchorObject); // Destroy the anchor object

        _rigidbody.isKinematic = false; // Turn off kinematic
        _rigidbody.useGravity = true; // Turn on gravity
        _rigidbody.velocity = initialVelocity; // Set the initial velocity of the snowball
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_fireOnCollision == (_fireOnCollision | (1 << other.gameObject.layer)))
        {
            // Do something
            Debug.Log("Hit something");
            Fire(_rigidbody.velocity);
        }
    }
}
