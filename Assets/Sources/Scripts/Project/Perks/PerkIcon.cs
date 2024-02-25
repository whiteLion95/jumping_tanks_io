using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PerkIcon : MonoBehaviour
{
    [SerializeField] private PerkIconData data;
    [SerializeField] private Perk myPerk;

    private RawImage rawImage;

    private void Awake()
    {
        if (myPerk.Duration < data.BlinkOffset)
            data.BlinkOffset = myPerk.Duration;

        rawImage = GetComponent<RawImage>();
    }

    private void OnEnable()
    {
        Invoke(nameof(Blink), myPerk.Duration - data.BlinkOffset);
    }

    private void Blink()
    {
        rawImage.DOFade(0f, data.BlinkSmoothness).SetLoops(-1, LoopType.Yoyo).SetEase(data.BlinkEase);
    }
}
