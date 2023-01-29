using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SphereCollider))]
public class RollingSpeedController : MonoBehaviour
{
    private Animator _animator;
    private SphereCollider _collider; // The collider of the snowball
    private bool _isRolling = true;
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
        if (_isRolling) // Turned off when the snowball is fired to prevent the animation from playing while the snowball is actually rolling using physics
        {
            UpdateRadius();
            UpdateAnimatorSpeed();
            UpdateLastPosition();
        }
    }
    public void StopRolling()
    {
        _animator.speed = 0;
        _isRolling = false;
    }
    public void StartRolling()
    {
        _isRolling = true;
    }
    private float GetSpeed()
    {
        return (transform.position - _lastPosition).magnitude / Time.deltaTime;
    }
    private void UpdateAnimatorSpeed()
    {
        _animator.speed = GetSpeed() / _radius;
    }

    private void UpdateRadius()
    {
        _radius = GetRadius(); // Calculate the radius of the snowball using global scale and local radius
    }

    private float GetRadius()
    {
        float radius = _collider.bounds.extents.x; // Calculate the radius of the snowball using global scale and local radius
        return radius;
    }

    private void UpdateLastPosition()
    {
        _lastPosition = transform.position;
    }

}
