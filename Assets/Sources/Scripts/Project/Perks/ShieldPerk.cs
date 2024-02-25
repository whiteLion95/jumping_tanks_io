using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPerk : Perk
{
    public static bool IsActive { get; private set; }

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
