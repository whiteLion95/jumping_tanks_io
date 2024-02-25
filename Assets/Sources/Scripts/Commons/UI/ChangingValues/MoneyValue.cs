using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoneyValue : MonoBehaviour
{
    public bool collectedOnLevel = false;
    public TMP_Text MoneyText;
    int currentMoney;

    private void Start()
    {
        StaticManager.onValueChange += SetMoneyValue;
        StaticManager.onRestart += Restart;

        if (!collectedOnLevel)
            currentMoney = StaticManager.instance.playerData.money;
        else
            currentMoney = StaticManager.moneyCollectedOnLevel;
        MoneyText.text = currentMoney + "";
    }

    public void SetMoneyValue()
    {
        if (!collectedOnLevel)
        {
            DOTween.To(() => currentMoney, x => currentMoney = x, StaticManager.instance.playerData.money, 0.6f)
            .OnUpdate(() => MoneyText.text = currentMoney + "");
        }
        else
        {
            if (StaticManager.instance.gameStatus != GameStatus.Menu)
                DOTween.To(() => currentMoney, x => currentMoney = x, StaticManager.moneyCollectedOnLevel, 0.6f)
                .OnUpdate(() => MoneyText.text = currentMoney + "");
        }
    }

    public void Restart()
    {
        currentMoney = 0;
        if (collectedOnLevel)
            MoneyText.text = "0";
    }
}
