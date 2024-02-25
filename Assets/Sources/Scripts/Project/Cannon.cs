using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;
using System;

public class Cannon : MonoBehaviour
{
    [SerializeField] private CannonData data;
    [SerializeField] private Projectile projectile;
    [SerializeField] private Collider colliderToIgnore;

    public Action<Cannon> OnShoot = delegate { };
    public Action OnReadyToShoot = delegate { };

    public List<Projectile> MyProjectiles { get; private set; } = new List<Projectile>();

    /// <summary>
    /// Determine whether or not a cannon finished reloading and ready to shoot
    /// </summary>
    private bool _readyToShoot = true;
    /// <summary>
    /// Wheter or not a cannon is allowed to shoot
    /// </summary>
    private bool _canShoot;
    private Projectile _originProj;
    private Character _myCharacter;

    public CannonData Data { get { return data; } }
    public uint ProjectilesPerShoot { get; set; } = 1;
    public float MultiShootsRate { get; set; }
    public bool MultiShoots { get; set; }

    private void Awake()
    {
        _originProj = projectile;
        _myCharacter = GetComponentInParent<Character>();
    }

    /// <summary>
    /// Get your approval to a cannon to be able to shoot
    /// </summary>
    public void StartShooting()
    {
        _canShoot = true;
        OnReadyToShoot?.Invoke();
    }

    /// <summary>
    /// Take away the ability to shoot from a cannon
    /// </summary>
    public void StopShooting()
    {
        _canShoot = false;
    }

    public void Shoot()
    {
        if (_canShoot && _readyToShoot)
        {
            if (!MultiShoots)
            {
                Projectile spawnedProj = SpawnProjectile();
                AddForceToProjectile(spawnedProj.Rb);
                OnShoot?.Invoke(this);
            }
            else
            {
                StartCoroutine(MultiShootsRoutine());
            }
        }
    }

    private IEnumerator MultiShootsRoutine()
    {
        OnShoot?.Invoke(this);

        for (int i = 0; i < ProjectilesPerShoot; i++)
        {
            Projectile spawnedProj = SpawnProjectile();
            AddForceToProjectile(spawnedProj.Rb);
            yield return new WaitForSeconds(MultiShootsRate);
        }
    }

    public void SetProjectileToSpawn(Projectile proj)
    {
        projectile = proj;
    }

    public void ResetProjectileToSpawn()
    {
        projectile = _originProj;
    }

    private Projectile SpawnProjectile()
    {
        Transform spawnedProjTrans = EZ_PoolManager.Spawn(projectile.transform, transform.position, transform.rotation);

        if (colliderToIgnore != null)
            Physics.IgnoreCollision(spawnedProjTrans.GetComponentInChildren<Collider>(), colliderToIgnore);

        Projectile spawnedProj = spawnedProjTrans.GetComponent<Projectile>();
        if (spawnedProj == null)
            Debug.LogError("Add script \"Projectile\" to your projectile gameobject");

        MyProjectiles.Add(spawnedProj);
        spawnedProj.MyCannon = this;
        spawnedProj.MyCharacter = _myCharacter;
        spawnedProj.OnProjDespawned += OnProjDespawnedHandler;
        StartCoroutine(ProjLifeCountDown(spawnedProj));
        StartCoroutine(Reload());

        return spawnedProj;
    }

    private IEnumerator ProjLifeCountDown(Projectile projectile)
    {
        yield return new WaitForSeconds(data.ProjLifeTime);
        EZ_PoolManager.Despawn(projectile.transform);
    }

    private void AddForceToProjectile(Rigidbody projRig)
    {
        projRig.AddForce(transform.forward * data.ShootPower, ForceMode.Impulse);
    }

    private IEnumerator Reload()
    {
        _readyToShoot = false;
        yield return new WaitForSeconds(data.ReloadTime);
        if (_canShoot)
        {
            _readyToShoot = true;
            OnReadyToShoot?.Invoke();
        }
    }

    private void OnProjDespawnedHandler(Projectile despawnedProj)
    {
        MyProjectiles.Remove(despawnedProj);

        if (colliderToIgnore != null)
            Physics.IgnoreCollision(despawnedProj.GetComponentInChildren<Collider>(), colliderToIgnore, false);

        despawnedProj.OnProjDespawned -= OnProjDespawnedHandler;
    }
}
