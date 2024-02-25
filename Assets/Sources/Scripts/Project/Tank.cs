using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class Tank : MonoBehaviour
{
    [SerializeField] private TankData data;

    [SerializeField] private Transform TankMesh;
    public static Action<Cannon> OnCannonShoot = delegate { };
    public static Action<Vector3> OnLanded = delegate { };

    private Cannon _cannon;
    private Rigidbody _rB;
    private bool _jumped;

    public Character Character { get; private set; }

    private void Awake()
    {
        _cannon = GetComponentInChildren<Cannon>();
        _rB = GetComponent<Rigidbody>();
        Character = GetComponent<Character>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _cannon.OnShoot += JumpBack;
    }

    private void JumpBack(Cannon shootingCannon)
    {
        Vector3 jumpDir = -_cannon.transform.forward + data.JumpDir;
        _rB.AddForce(jumpDir.normalized * _cannon.Data.RecoilPower, ForceMode.Impulse);
        _jumped = true;
        
        
        OnCannonShoot?.Invoke(_cannon);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && _jumped)
        {
            TankMesh.DOShakeRotation(0.5f, new Vector3(7, 7, 7), 10, 45);
            _jumped = false;
            OnLanded?.Invoke(transform.position);
        }
    }
}
