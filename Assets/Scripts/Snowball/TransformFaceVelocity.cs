using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFaceVelocity : MonoBehaviour
{
    private Vector3 _lastPosition;

    private void Start()
    {
        // Store the starting position of the object
        _lastPosition = transform.position;
    }
    private void Update()
    {
        Vector3 velocity = GetVelocity();
        if (velocity.magnitude == 0) // don't rotate if not moving
        {
            return;
        }

        transform.rotation = Quaternion.LookRotation(velocity);
        _lastPosition = transform.position;
    }
    private Vector3 GetVelocity()
    {
        return (transform.position - _lastPosition) / Time.deltaTime;
    }
}