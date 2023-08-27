using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

        switch (_submarineUpgrades._UpgradeType)
        {
            case SubmarineUpgrades.UpgradeType.HullUpgrade:
                _submarineUpgrades._HullUpgradeAmount = EditorGUILayout.FloatField("Hull Upgrade", _submarineUpgrades._HullUpgradeAmount);
                _submarineUpgrades._Cost = EditorGUILayout.IntField("Cost", _submarineUpgrades._Cost);
                break;
            case SubmarineUpgrades.UpgradeType.OxygenUpgrade:
                _submarineUpgrades._OxygenUpgradeAmount = EditorGUILayout.FloatField("Oxygen Upgrade", _submarineUpgrades._OxygenUpgradeAmount);
                _submarineUpgrades._Cost = EditorGUILayout.IntField("Cost", _submarineUpgrades._Cost);
                break;
            case SubmarineUpgrades.UpgradeType.SonarUpgrade:
                _submarineUpgrades._SonarUpgradeAmount = EditorGUILayout.FloatField("Sonar Upgrade", _submarineUpgrades._SonarUpgradeAmount);
                _submarineUpgrades._Cost = EditorGUILayout.IntField("Cost", _submarineUpgrades._Cost);
                break;
            case SubmarineUpgrades.UpgradeType.SpeedUpgrade:
                _submarineUpgrades._SpeedUpgradeAmount = EditorGUILayout.FloatField("Speed Upgrade", _submarineUpgrades._SpeedUpgradeAmount);
                _submarineUpgrades._Cost = EditorGUILayout.IntField("Cost", _submarineUpgrades._Cost);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
