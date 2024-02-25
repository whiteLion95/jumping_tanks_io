using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowLevelTriggers : MonoBehaviour
{
    private Enemy myEnemy;
    private bool onOrUnderOtherTank;

    private void Awake()
    {
        myEnemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            Projectile shootingProjectile = other.GetComponentInParent<Projectile>();

            if (shootingProjectile != null && !onOrUnderOtherTank)
            {
                Character.OnShooted?.Invoke(shootingProjectile, myEnemy);
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Tank"))
        {
            Tank collidedTank = other.GetComponent<Tank>();

            if (collidedTank != null)
            {
                if (!collidedTank.Equals(myEnemy.GetComponent<Tank>()))
                {
                    onOrUnderOtherTank = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Tank"))
        {
            Tank collidedTank = other.GetComponent<Tank>();

            if (collidedTank != null)
            {
                if (!collidedTank.Equals(myEnemy.GetComponent<Tank>()))
                {
                    onOrUnderOtherTank = false;
                }
            }
        }
    }
}
