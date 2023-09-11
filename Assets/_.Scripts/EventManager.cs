using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static Action<bool> onPowerUP;
    public static Action<int> onStarCollect;
    public static Action onGameOver;
}
