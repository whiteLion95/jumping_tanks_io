using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deslab.Level;

public class PerksManager : MonoBehaviour
{
    [SerializeField] private List<Perk> perks;

    private void Awake()
    {
        LevelManager.OnLevelLoaded += ResetAllPerks;
    }

    private void ResetAllPerks()
    {
        foreach (Perk perk in perks)
        {
            perk.ResetPerk();
        }
    }
}
