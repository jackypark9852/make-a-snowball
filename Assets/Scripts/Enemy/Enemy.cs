using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float speed = 5f;
    private const float Delta = 1f;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        var currentPosition = transform.position;
        var targetDir = playerTransform.position - currentPosition;
        targetDir.y = 0f;
        if (targetDir.magnitude < Delta)
        {
            // do nothing
            return; 
        }
        
        _rigidBody.MovePosition(Vector3.MoveTowards(currentPosition,
            targetDir.normalized + currentPosition, speed * Time.deltaTime));
        _rigidBody.MoveRotation(
            Quaternion.RotateTowards(
                transform.rotation, 
                Quaternion.LookRotation(targetDir),
                2f
                )
            );

    }
}
