using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static Action OnPlayerDetected;
    public static Action OnGetPebble;
    public static Action<float> OnEnemyDistracted;
}
