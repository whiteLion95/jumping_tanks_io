using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]

public class CharacterData : ScriptableObject
{
    [SerializeField] protected uint maxHealth;

    public int MaxHealth { get { return (int)maxHealth; } }
}
