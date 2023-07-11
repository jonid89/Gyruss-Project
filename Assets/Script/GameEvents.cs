using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

#region Singleton
    public static GameEvents Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

#endregion

    public delegate void LeftClickDown();
    public static LeftClickDown OnLeftClickDown;
    public delegate void LeftClickHold();   
    public static LeftClickHold OnLeftClickHold;
    public delegate void LeftClickUp();
    public static LeftClickUp OnLeftClickUp;
    public delegate void RightClickDown();
    public static RightClickDown OnRightClickDown;
    public delegate void RightClickUp();
    public static RightClickUp OnRightClickUp;

    public static void Initialize()
    {
        Instance = new GameEvents();
    }

    public void TriggerLeftClickDown()
    {
        if(OnLeftClickDown != null)
            OnLeftClickDown();
    }

    public void TriggerLeftClickHold()
    {
        if(OnLeftClickHold != null)
            OnLeftClickHold();
    }

    public void TriggerLeftClickUp()
    {
        if(OnLeftClickUp != null)
            OnLeftClickUp();
    }

    public void TriggerRightClickDown()
    {
        if(OnRightClickDown != null)
            OnRightClickDown();
    }

    public void TriggerRightClickUp()
    {
        if(OnRightClickUp != null)
            OnRightClickUp();
    }
}





