using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnowballCollision : MonoBehaviour
{
    public UnityEvent<Collision> CollisionEntered;

    private void OnCollisionEnter(Collision other)
    {
        CollisionEntered.Invoke(other);
    }
}
