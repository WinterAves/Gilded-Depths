using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
}

[CustomEditor(typeof(SubmarineUpgrades)), CanEditMultipleObjects]
public class SubmarineUpgradesEditor : Editor
{
    private SubmarineUpgrades _submarineUpgrades;

    private void OnEnable()
    {
        _submarineUpgrades = (SubmarineUpgrades)target;
    }

    public override void OnInspectorGUI()
    {
        _submarineUpgrades._UpgradeType = (SubmarineUpgrades.UpgradeType)EditorGUILayout.EnumPopup("Upgrade Type", _submarineUpgrades._UpgradeType);

        switch(_submarineUpgrades._UpgradeType)
        {
            case SubmarineUpgrades.UpgradeType.HullUpgrade:
                _submarineUpgrades._HullUpgradeAmount = EditorGUILayout.FloatField("Hull Upgrade",_submarineUpgrades._HullUpgradeAmount);
                _submarineUpgrades._OxygenUpgradeAmount = 0f;
                _submarineUpgrades._SonarUpgradeAmount = 0f;
                _submarineUpgrades._SpeedUpgradeAmount = 0f;
                break;
            case SubmarineUpgrades.UpgradeType.OxygenUpgrade:
                _submarineUpgrades._OxygenUpgradeAmount = EditorGUILayout.FloatField("Oxygen Upgrade", _submarineUpgrades._OxygenUpgradeAmount);
                _submarineUpgrades._HullUpgradeAmount = 0f;
                _submarineUpgrades._SonarUpgradeAmount = 0f;
                _submarineUpgrades._SpeedUpgradeAmount = 0f;
                break;
            case SubmarineUpgrades.UpgradeType.SonarUpgrade:
                _submarineUpgrades._SonarUpgradeAmount = EditorGUILayout.FloatField("Sonar Upgrade", _submarineUpgrades._SonarUpgradeAmount);
                _submarineUpgrades._HullUpgradeAmount = 0f;
                _submarineUpgrades._OxygenUpgradeAmount = 0f;
                _submarineUpgrades._SpeedUpgradeAmount = 0f;
                break;
            case SubmarineUpgrades.UpgradeType.SpeedUpgrade:
                _submarineUpgrades._SpeedUpgradeAmount = EditorGUILayout.FloatField("Speed Upgrade", _submarineUpgrades._SpeedUpgradeAmount);
                _submarineUpgrades._HullUpgradeAmount = 0f;
                _submarineUpgrades._OxygenUpgradeAmount = 0f;
                _submarineUpgrades._SonarUpgradeAmount = 0f;
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}