using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineStatus : MonoBehaviour
{
    [SerializeField] private float _hullStrength = 20f;     // Health?
    [SerializeField] private float _maxOxygen = 100f;
    [SerializeField] private float _sonarRange = 10f;
    [SerializeField] private float _maxSpeed = 3f;

    private float _currentOxygen = 100f;

    void Update()
    {
        //DecreaseOxygen();
    }

    private void DecreaseOxygen()
    {
        _currentOxygen -= Time.deltaTime;
    }

    private void UpgradeSubmarine(SubmarineUpgrades upgrade)
    {
        switch(upgrade._UpgradeType)
        {
            case SubmarineUpgrades.UpgradeType.HullUpgrade:
                _hullStrength += upgrade._HullUpgradeAmount;
                break;
            case SubmarineUpgrades.UpgradeType.OxygenUpgrade:
                _maxOxygen += upgrade._OxygenUpgradeAmount;
                break;
            case SubmarineUpgrades.UpgradeType.SonarUpgrade:
                _sonarRange += upgrade._SonarUpgradeAmount;
                break;
            case SubmarineUpgrades.UpgradeType.SpeedUpgrade:
                _maxSpeed += upgrade._SpeedUpgradeAmount;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Upgrade")
        {
            SubmarineUpgrades upgrade = other.GetComponent<SubmarineUpgrades>();
            UpgradeSubmarine(upgrade);
        }
    }
}
