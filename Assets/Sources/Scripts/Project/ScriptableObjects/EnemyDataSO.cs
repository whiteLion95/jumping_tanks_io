using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deslab.Level;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "ScriptableObjects/EnemyDataSO")]

public class EnemyDataSO : CharacterData
{
    /// <summary>
    /// Sets max health depending on the current level
    /// </summary>
    public void DetermineMaxHealth()
    {
        int currentLevel = StaticManager.levelID;

        if (currentLevel <= 3)
            maxHealth = 2;
        else if (currentLevel <= 6)
            maxHealth = 3;
        else if (currentLevel <= 9)
            maxHealth = 4;
        else if (currentLevel <= 15)
            maxHealth = 5;
        else if (currentLevel > 15)
            maxHealth = 6;
    }
}
