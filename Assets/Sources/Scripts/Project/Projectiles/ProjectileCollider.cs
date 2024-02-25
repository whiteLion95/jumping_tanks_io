using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProjectileCollider : MonoBehaviour
{
    public Action OnProjectilesCollision = delegate { };

    public Projectile MyProjectile { get; private set; }

    private void Awake()
    {
        MyProjectile = GetComponentInParent<Projectile>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            Projectile collidedProj = collision.gameObject.GetComponentInParent<Projectile>();

            if (collidedProj != null)
            {
                if (MyProjectile.MyCannon.Equals(collidedProj.MyCannon))
                    return;
            }

            OnProjectilesCollision?.Invoke();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Shield") && !(MyProjectile.MyCharacter is Player))
        {
            OnProjectilesCollision?.Invoke();
        }
    }
}
