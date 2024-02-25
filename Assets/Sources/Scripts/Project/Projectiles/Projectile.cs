using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;
using System;

public class Projectile : MonoBehaviour, IPoolable
{
    [SerializeField] private uint damage;

    public static Action<ProjectileCollider> OnExplode = delegate { };
    public Action<Projectile> OnProjDespawned = delegate { };

    private ProjectileCollider _myCollider;

    public Rigidbody Rb { get; private set; }
    public int Damage { get { return (int)damage; } }
    public Cannon MyCannon { get; set; }
    public Character MyCharacter { get; set; }

    private void Awake()
    {
        Rb = GetComponentInChildren<Rigidbody>();
        _myCollider = GetComponentInChildren<ProjectileCollider>();
        _myCollider.OnProjectilesCollision += OnProjectilesCollisionHandler;

        if (Rb == null)
        {
            Debug.LogWarning("At least one children of a projectile must have a Rigidbody component on it");
        }
    }

    public void OnDespawned()
    {
        ResetRigidbody();
        Rb.gameObject.transform.localPosition = Vector3.zero;
        Character.OnShooted -= OnCharacterShootedHandler;
        OnProjDespawned?.Invoke(this);
    }

    public void OnSpawned()
    {
        Character.OnShooted += OnCharacterShootedHandler;
    }

    private void ResetRigidbody()
    {
        if (Rb != null)
        {
            Rb.velocity = Vector3.zero;
            Rb.angularVelocity = Vector3.zero;
            Rb.ResetCenterOfMass();
            Rb.ResetInertiaTensor();
        }
    }

    private void OnCharacterShootedHandler(Projectile shootingProjectile, Character shootedCharacter)
    {
        if (shootingProjectile.Equals(this))
        {
            Explode();
        }
    }

    private void OnProjectilesCollisionHandler()
    {
        Explode();
    }

    private void Explode()
    {
        OnExplode?.Invoke(_myCollider);

        EZ_PoolManager.Despawn(transform);
    }
}
