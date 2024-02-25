using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class DecalsManager : MonoBehaviour
{
    [SerializeField] private Transform crackDecal;
    [SerializeField] private Vector3 offset;

    private void OnEnable()
    {
        Tank.OnLanded += OnTankLandedHandler;
    }

    private void OnDisable()
    {
        Tank.OnLanded -= OnTankLandedHandler;
    }

    private void OnTankLandedHandler(Vector3 landPos)
    {
        EZ_PoolManager.Spawn(crackDecal, landPos + offset, Quaternion.identity);
    }
}
