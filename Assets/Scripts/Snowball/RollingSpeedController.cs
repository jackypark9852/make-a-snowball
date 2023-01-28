using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RollingSpeedController : MonoBehaviour
{
    private Vector3 _lastPosition;
    private Animator _animator;
    // Start is called before the first frame update
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _lastPosition = transform.position;
    }

    private void Update()
    {
        float speed = GetSpeed();
        _animator.speed = speed; // Set the speed parameter in the animator so that snowball rolls at correct speed
        _lastPosition = transform.position;
    }


    private float GetSpeed()
    {
        return (transform.position - _lastPosition).magnitude / Time.deltaTime;
    }
}
