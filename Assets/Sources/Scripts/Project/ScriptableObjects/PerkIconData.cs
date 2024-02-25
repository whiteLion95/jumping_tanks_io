using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "PerkIconData", menuName = "ScriptableObjects/PerkIconData")]
public class PerkIconData : ScriptableObject
{
    [SerializeField] [Range(0f, 30f)] private float blinkOffset = 5f;
    [SerializeField] private Ease blinkEase = Ease.Linear;
    [SerializeField] private float blinkSmoothness = 0.5f;

    public float BlinkOffset { get { return blinkOffset; } set { blinkOffset = value; } }
    public Ease BlinkEase { get { return blinkEase; } }
    public float BlinkSmoothness { get { return blinkSmoothness; } }
}
