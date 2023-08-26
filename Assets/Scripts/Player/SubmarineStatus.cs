using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SubmarineStatus : MonoBehaviour
{
    public static SubmarineStatus Instance { get; private set; }

    [SerializeField] private Image _oxygenBar;
    [SerializeField] private GameObject _sonar;
    [SerializeField] private Transform _childTransform;

    [Header("Upgrades")]
    [SerializeField] private float _hullStrength = 20f;     // Health?
    [SerializeField] private float _maxOxygen = 100f;
    [SerializeField] private float _sonarRange = 10f;

    [Header("Oxygen")]
    [SerializeField] private Gradient _gradient = new Gradient();
    [SerializeField] private float _oxygenDepletionSpeed = 5f;
    [SerializeField] private float _oxygenRefillSpeed = 10f;
    private float _currentOxygen = 100f;
    private bool _refillingOxygen = false;
    private bool _shopping = false;

    private SubmarineMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<SubmarineMovement>();

        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (!_refillingOxygen && !_shopping)
            DecreaseOxygen();
        else if (_refillingOxygen)
            RefillOxygen();

        if (Input.GetKeyDown(KeyCode.Mouse1))
            ActivateSonar();
    }

    #region Hull
    public void TakeDamage(float damage)
    {
        _hullStrength -= damage;
    }
    #endregion

    #region Oxygen
    private void RefillOxygen()
    {
        _currentOxygen = Mathf.Clamp(_currentOxygen + (_maxOxygen / 100) * Time.deltaTime * _oxygenRefillSpeed, 0f, _maxOxygen);
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

    #region Sonar
    private void ActivateSonar()
    {
        GameObject newSonar = Instantiate(_sonar, transform.position, Quaternion.identity);
        newSonar.GetComponent<Sonar>().ActivateSonar(_childTransform.position, _sonarRange);
    }
    #endregion

    #region Upgrades
    public void UpgradeSubmarine(SubmarineUpgrades upgrade)
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
                float newSpeed = _movement.UpgradeSpeed(upgrade._SpeedUpgradeAmount);
                Debug.Log($"Speed upgraded, status: {newSpeed}");
                break;
        }
    }

    private void SetShopping()
    {
        _shopping = true;
        _movement.SetShopping();
    }

    // Resumes depletion of oxygen
    public void UnsetShopping()
    {
        _shopping = false;
        _movement.UnsetShopping();
    }
    #endregion

    #region Collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Crashed Into Obstacle");
        }
        
        if (collision.gameObject.tag == "Shop")
        {
            collision.gameObject.GetComponent<Shop>().SlideInPanel();
            SetShopping();
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
