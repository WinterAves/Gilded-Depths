using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineUpgrades : MonoBehaviour
{
    public enum UpgradeType 
    { 
      HullUpgrade, 
      OxygenUpgrade, 
      SonarUpgrade,
      SpeedUpgrade
    };

    public UpgradeType _UpgradeType;
    public float _HullUpgradeAmount;
    public float _OxygenUpgradeAmount;
    public float _SonarUpgradeAmount;
    public float _SpeedUpgradeAmount;
    public int _Cost;
}