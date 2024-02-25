using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballPerk : Perk
{
    [SerializeField] private Projectile fireBallProj;

    public static bool IsActive { get; private set; }

    public Projectile FireBallProj { get { return fireBallProj; } }

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
