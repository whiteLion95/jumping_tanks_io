using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFocus : CamFocusCore
{
    private void Awake()
    {
        Player.OnLoaded += OnPlayerLoadedHandler;
        //StaticManager.OnLose += OnLevelEndedHanler;
        //StaticManager.OnWin += OnLevelEndedHanler;
    }

    private void OnPlayerLoadedHandler()
    {
        StartFollowing(Player.Instance.transform);
    }

    //private void OnLevelEndedHanler()
    //{
    //    StopFollowing();
    //}
}
