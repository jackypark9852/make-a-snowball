using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballlLogic : MonoBehaviour
{
    [Tooltip("The layers that the snowball will fire on when it collides with them")]
    [SerializeField] private LayerMask _fireOnCollision;
    [SerializeField] private PhysicMaterial _firedPhysicsMaterial;
    [Tooltip("The model of the snowball/ gameobject that holds the rigidbody and collider")]
    [SerializeField] private GameObject _model;
    [SerializeField] private float _growthRateBeforeFire = 1.0f;
    [SerializeField] private float _growthRateAfterFire = 1.0f;
    [SerializeField] private float _maxRadius = 10.0f;
    private RollingSpeedController _rollingSpeedController;
    private SnowballCollision _snowballCollision;
    private Rigidbody _rigidbody;
    private Collider _collider;
    private bool _isFired = false;
    private Vector3 _lastPosition;
    private Vector3 _velocity;
    private void Awake()
    {
        _rollingSpeedController = _model.GetComponent<RollingSpeedController>();
        _rigidbody = _model.GetComponent<Rigidbody>();
        _collider = _model.GetComponent<Collider>();
        _snowballCollision = _model.GetComponent<SnowballCollision>();

        _snowballCollision.CollisionEntered.AddListener(OnCollisionEnter); // Relay the collision event to the OnCollisionEnter method
    }

    private void Start()
    {
        _lastPosition = transform.position;
    }

    private void Update()
    {
        _velocity = (transform.position - _lastPosition) / Time.deltaTime;
        _lastPosition = transform.position;
    }
    public void StartRolling()
    {
        _rollingSpeedController.StartRolling();
    }

    public void Fire(Vector3 initialVelocity)
    { // Called by the player when the snowball is fired
        transform.parent = null; // Detach the snowball from the player

        _rigidbody.isKinematic = false; // Turn off kinematic
        _rigidbody.useGravity = true; // Turn on gravity
        _rigidbody.velocity = initialVelocity; // Set the initial velocity of the snowball
        _collider.material = _firedPhysicsMaterial; // Set the physics material of the snowball to the fired physics material
    }

    public float GetRadius()
    {
        return _collider.bounds.extents.x; // Calculate the radius of the snowball using global scale and local radius
    }

    public void OnCollisionEnter(Collision other)
    {
        if (_fireOnCollision == (_fireOnCollision | (1 << other.gameObject.layer)))
        {
            if (!_isFired)
            {
                _isFired = true;
                Fire(_velocity);
            }
        }
    }

    private void UpdateSize()
    {
        float radius = GetRadius();
        if (radius < _maxRadius)
        {
            float growthRate = _isFired ? _growthRateAfterFire : _growthRateBeforeFire;
            float newRadius = radius + growthRate * Time.deltaTime;
            float scale = newRadius / radius;
            transform.localScale *= scale;
        }
    }
}