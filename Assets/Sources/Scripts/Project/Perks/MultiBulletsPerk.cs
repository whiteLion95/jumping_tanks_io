using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBulletsPerk : Perk
{
    [SerializeField] private uint bulletsCount = 3;
    [SerializeField] [Range(0f, 0.5f)] private float fireRate = 0.2f;
    public static bool IsActive { get; private set; }

    public uint BulletsCount { get { return bulletsCount; } }
    public float FireRate { get { return fireRate; } }

    protected override void ActionsOnTrigger()
    {
        base.ActionsOnTrigger();
        IsActive = true;
    }

    protected override void EndEffect()
    {
        base.EndEffect();
        IsActive = false;
    }

    public override void ResetPerk()
    {
        base.ResetPerk();
        IsActive = false;
    }
}
