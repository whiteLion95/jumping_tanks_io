using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Deslab.UI
{
    public class WinScreen : CanvasGroupWindow
    {
        [SerializeField] private Button claimButton;

        [SerializeField] private Header _header;

        [SerializeField] private Reward _reward;
        
        public List<EndGameStars> endGameStars = new List<EndGameStars>();

        private void Awake()
        {
            _header.Init();
            _reward.Init();
            claimButton.onClick.AddListener(() =>
            {
                if (!StaticManager.instance.debugMode)
                    StaticManager.AddMoney(StaticManager.moneyCollectedOnLevel);
                claimButton.interactable = false;
                claimButton.transform.localScale = Vector3.zero;
                StaticManager.victory = true;
                StaticManager.LoadNextLevel();
                UIManager.instance.ShowMenu();
                //StaticManager.SavePlayerData();
                StaticManager.Restart();
            });
        }

        public override void ShowWindow(Action onCompleted = null)
        {
            ResetParameters();

            base.ShowWindow(() =>
            {
                _header.PlayAnimation();
                _reward.PlayAnimation(ClaimCoins);
                StaticManager.SuccessVibrate();
                claimButton.interactable = true;
            });

            void ResetParameters()
            {
                _header.ResetValues();
                _reward.ResetValues();
                claimButton.transform.localScale = Vector3.zero;
            }
        }

        public override void HideWindow()
        {
            base.HideWindow(() =>
            {
                claimButton.transform.localScale = Vector3.zero;
                for (int i = 0; i <= endGameStars.Count; i++)
                {
                    endGameStars[i].ResetStar();
                }
            });
        }

        private void ClaimCoins()
        {
            SetEarnings(StaticManager.moneyCollectedOnLevel);
        }
        
        private void SetEarnings(int moneyValue, float multiplier = 1)
        {
            float money = moneyValue;
            if (multiplier > 1)
            {
                money *= multiplier;
            }

            float currentCoinsVal = int.Parse(_reward.rewardLabel.text);
            StaticManager.moneyCollectedOnLevel = (int) money;
            DOTween.To(() => currentCoinsVal, x => currentCoinsVal = x, money, 0.3f).OnUpdate(() =>
            {
                currentCoinsVal = (int) Mathf.Round(currentCoinsVal);
                _reward.rewardLabel.text = currentCoinsVal + "";
            });

            claimButton.transform.DOScale(1, 0.5f)
                .SetDelay(fadeTime + 2f)
                .SetEase(Ease.OutBack);
        }
    }
}