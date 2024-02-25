using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deslab.Level;

public class BalanceManager : MonoBehaviour
{
    [SerializeField] private EnemyDataSO enemyData;

    private void OnEnable()
    {
        LevelManager.OnLevelLoaded += OnLevelLoadedHandler;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelLoaded -= OnLevelLoadedHandler;
    }

    private void OnLevelLoadedHandler()
    {
        enemyData.DetermineMaxHealth();
    }
}
