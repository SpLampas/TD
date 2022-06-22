using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Actions
{
   public static Action<int> OnDeath;
   public static Action<int, float> OnNewWave;
   public static Action OnStageClear;
   public static Action OnEmptyBank;
   public static Action OnEnemyReached;
}
