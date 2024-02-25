using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Tooltip("Range within which random time to shoot will be generated")] [Range(0f, 5f)] [SerializeField] private float randTimeRange = 2f;
    [Tooltip("Until what level player will be able to hit enemies even when projectiles miss them by Y axes")] [SerializeField] private int easyLevelsCount;

    public float RandTimeRange { get { return randTimeRange; } }
    public int EasyLevelsCount { get { return easyLevelsCount; } }
}
