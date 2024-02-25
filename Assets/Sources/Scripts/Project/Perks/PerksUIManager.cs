using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerksUIManager : MonoBehaviour
{
    public static PerksUIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public PerkIcon SpawnIcon(PerkIcon icon)
    {
        return Instantiate(icon, transform);
    }
}
