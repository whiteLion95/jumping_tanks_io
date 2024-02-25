using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using Deslab.Level;

/// <summary>
/// A base class for perks
/// </summary>
public class Perk : CollectionTrigger
{
    [SerializeField] [Range(0f, 30f)] protected float duration = 15f;
    [SerializeField] private PerkIcon perkIcon;
    
    public static Action<Perk> OnTrigger = delegate { };
    public static Action<Perk> OnEnd = delegate { };

    protected Player player;
    protected PerksUIManager perksUI;
    protected PerkIcon spawnedIcon;

    public float Duration { get { return duration; } }

    private void Start()
    {
        player = Player.Instance;
        perksUI = PerksUIManager.Instance;
    }

    protected override void ActionsOnTrigger()
    {
        OnTrigger?.Invoke(this);
        spawnedIcon = perksUI.SpawnIcon(perkIcon);
        Invoke(nameof(EndEffect), duration);
    }

    protected virtual void EndEffect()
    {
        OnEnd?.Invoke(this);
        Destroy(spawnedIcon.gameObject);
    }

    public virtual void ResetPerk() { }
}
