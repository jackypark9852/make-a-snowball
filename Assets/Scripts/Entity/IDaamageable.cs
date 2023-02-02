using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public Vector3 KnockbackDirection { get; set; }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        Health = Mathf.Min(Health, MaxHealth);

        OnHit();
        if (Health <= 0f)
        {
            Health = 0f;
            OnDeath();
        }
        OnHealthUpdate(damage);
    }

    public virtual void Heal(float amount)
    {
        Health = Mathf.Min(Health + amount, MaxHealth);
        OnHealthUpdate(-amount);
    }

    public virtual void OnHit() { }

    public virtual void OnHealthUpdate(float damage) { }

    public virtual void TakeKnockback(Vector3 displacement)
    {
        return;
    }

    public void SetKnockbackDirection(Vector3 direction)
    {
        direction.y = 0.0f;
        KnockbackDirection = direction.normalized;
    }
    
    public abstract void OnDeath();
}
