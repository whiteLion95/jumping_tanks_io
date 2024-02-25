using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CannonData", menuName = "ScriptableObjects/CannonData")]
public class CannonData : ScriptableObject
{
    [Tooltip("Reloading duration")] [Range(0f, 10f)] [SerializeField] private float reloadTime = 0.5f;
    [Tooltip("Force that will be applied to the shooted projectile")] [Range(0f, 500f)] [SerializeField] private float shootPower;
    [Tooltip("Time duration after which a spawned projectile will be despawned")] [Range(0f, 50f)] [SerializeField] private float projLifeTime;
    [Tooltip("Force that will be applied back to a tank")] [Range(0f, 500f)] [SerializeField] private float recoilPower;
    //[Tooltip("Speed of the projectile from the cannon")] [Range(0f, 10f)] [SerializeField] private float projectileSpeed;
    //[Tooltip("Distance that the projectile from the cannon can get to")] [Range(0f, 50f)] private float fireRange;

    public float ReloadTime { get { return reloadTime; } }
    public float ShootPower { get { return shootPower; } }
    public float ProjLifeTime { get { return projLifeTime; } }
    public float RecoilPower { get { return recoilPower; } }

    //public float ProjectileSpeed { get { return projectileSpeed; } }
    //public float FireRange { get { return fireRange; } }
}
