using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SubmarineStatus : MonoBehaviour
{
    [SerializeField] private Image _oxygenBar;

    [Header("Upgrades")]
    [SerializeField] private float _hullStrength = 20f;     // Health?
    [SerializeField] private float _maxOxygen = 100f;
    [SerializeField] private float _sonarRange = 10f;
    [SerializeField] private float _maxSpeed = 3f;

    [Header("Oxygen")]
    [SerializeField] private Gradient _gradient = new Gradient();
    [SerializeField] private float _oxygenDepletionSpeed = 5f;
    [SerializeField] private float _oxygenRefillSpeed = 10f;
    private float _currentOxygen = 100f;
    private bool _refillingOxygen = false;

    void Update()
    {
        if (!_refillingOxygen)
            DecreaseOxygen();
        else
            RefillOxygen();
    }

    #region Oxygen
    private void RefillOxygen()
    {
        _currentOxygen = Mathf.Clamp(_currentOxygen + Time.deltaTime * _oxygenRefillSpeed, 0f, _maxOxygen);
        UpdateOxygenBar();
    }

    private void DecreaseOxygen()
    {
        _currentOxygen = Mathf.Clamp(_currentOxygen - Time.deltaTime * _oxygenDepletionSpeed, 0f, _maxOxygen);
        UpdateOxygenBar();
    }

    private void UpdateOxygenBar()
    {
        float percentage = _currentOxygen / _maxOxygen;
        _oxygenBar.fillAmount = percentage;
        _oxygenBar.color = _gradient.Evaluate(percentage);
    }
    #endregion

    #region Upgrades
    private void UpgradeSubmarine(SubmarineUpgrades upgrade)
    {
        switch(upgrade._UpgradeType)
        {
            case SubmarineUpgrades.UpgradeType.HullUpgrade:
                _hullStrength += upgrade._HullUpgradeAmount;
                Debug.Log($"Hull upgraded, status: {_hullStrength}");
                break;

            case SubmarineUpgrades.UpgradeType.OxygenUpgrade:
                _maxOxygen += upgrade._OxygenUpgradeAmount;
                Debug.Log($"Oxygen tank upgraded, status: {_maxOxygen}");
                break;

            case SubmarineUpgrades.UpgradeType.SonarUpgrade:
                _sonarRange += upgrade._SonarUpgradeAmount;
                Debug.Log($"Sonar upgraded, status: {_sonarRange}");
                break;

            case SubmarineUpgrades.UpgradeType.SpeedUpgrade:
                _maxSpeed += upgrade._SpeedUpgradeAmount;
                Debug.Log($"Speed upgraded, status: {_maxSpeed}");
                break;
        }
    }
    #endregion

    #region Collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Crashed Into Obstacle");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Upgrade")
        {
            Debug.Log("Upgrade Retrieved");

            SubmarineUpgrades upgrade = collision.gameObject.GetComponent<SubmarineUpgrades>();
            UpgradeSubmarine(upgrade);
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "OxygenRefill")
        {
            _refillingOxygen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "OxygenRefill")
        {
            _refillingOxygen = false;
        }
    }
    #endregion
}
