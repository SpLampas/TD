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
   public static Action<int> OnChangeTower;
   public static Action OnNotEnoughGold;
   public static Action<Vector3> OnMakePlacableAgain;
}
