using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TankData", menuName = "ScriptableObjects/TankData")]

public class TankData : ScriptableObject
{
    [Tooltip("This will determine the angle by Y axis of jumping back")][Range(0f, 5f)][SerializeField] private float yJumpAdjustment;

    public Vector3 JumpDir { get { return new Vector3(0, yJumpAdjustment, 0); } }
}
