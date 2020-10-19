using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class GameEvents
{
    public static Action OnGameStart = delegate { };
    public static Action OnGameFinish = delegate { };

    public static void TriggerOnGameStart() {
        OnGameStart?.Invoke();
    }

    public static void TriggerOnGameFinish() {
        OnGameFinish?.Invoke();
    }
}
