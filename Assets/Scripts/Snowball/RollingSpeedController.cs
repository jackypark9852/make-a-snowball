using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SphereCollider))]
public class RollingSpeedController : MonoBehaviour
{
    private Animator _animator;
    private SphereCollider _collider; // The collider of the snowball
    private float _radius;
    private Vector3 _lastPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        UpdateLastPosition();
        UpdateRadius();
    }

    private void Update()
    {
        UpdateRadius();
        float speed = GetSpeed();
        _animator.speed = speed / _radius; // Set the speed parameter in the animator so that snowball rolls at correct speed
        UpdateLastPosition();
    }
    private float GetSpeed()
    {
        return (transform.position - _lastPosition).magnitude / Time.deltaTime;
    }

    private void UpdateRadius()
    {
        _radius = transform.lossyScale.x * _collider.radius; // Calculate the radius of the snowball using global scale and local radius
    }

    private void UpdateLastPosition()
    {
        _lastPosition = transform.position;
    }
}
