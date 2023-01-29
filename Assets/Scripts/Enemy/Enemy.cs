using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float speed = 5f;
    private const float Delta = 1f;
    private Rigidbody _rigidBody;
    private bool _enabled = true;
    

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (!enabled)
        {
            return;
        }
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

    private void OnTriggerEnter(Collider other)
    {
        // get a snowball
        if (other.gameObject.CompareTag("Snowball"))
        {
            _enabled = false;
            Destroy(_rigidBody);
            Destroy(GetComponent<CapsuleCollider>());
            transform.parent = other.transform;
            var posMag = transform.localPosition.magnitude;
            var posNorm = transform.localPosition.normalized;
            // now it is 1/3 the original distance from snowball center
            transform.localPosition = (posMag / 3f) * posNorm;
            Destroy(GetComponent<Enemy>());
        } else if (other.GetComponent<PlayerController>() != null)
        {
            // hurt player
        }
    }
}
