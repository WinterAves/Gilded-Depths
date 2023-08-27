using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubmarineStatus : MonoBehaviour
{
    public static SubmarineStatus Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _hullDisplay;
    [SerializeField] private TextMeshProUGUI _moneyDisplay;
    [SerializeField] private Slider _oxygenMeter;
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

    private GameObject _currentSonar;

    [Space]
    [SerializeField] private int _money = 100;
    [SerializeField] private int _moneyIncreaseAmount = 50;

    private SubmarineMovement _movement;
    private bool _dead = false;

    private void Awake()
    {
        _movement = GetComponent<SubmarineMovement>();

        if(Instance == null)
            Instance = this;

        UpdateHull();
        UpdateMoney();
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

    public void Die()
    {
        if(!_dead)
        {
            _movement.Die();
            Debug.Log("Dead");
        }
    }

    #region Hull
    public void TakeDamage(float damage)
    {
        _hullStrength = Mathf.Clamp(_hullStrength - damage, 0, Mathf.Infinity);

        if (_hullStrength == 0) 
            Die();

        UpdateHull();
    }

    private void UpdateHull()
    {
        _hullDisplay.text = $"Hull: {_hullStrength}";
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
        if (_currentOxygen == 0) Die();
        UpdateOxygenBar();
    }

    private void UpdateOxygenBar()
    {
        float percentage = _currentOxygen / _maxOxygen;
        _oxygenMeter.value = percentage;
    }
    #endregion

    #region Sonar
    private void ActivateSonar()
    {
        if (_currentSonar != null) return;

        _currentSonar = Instantiate(_sonar, transform.position, Quaternion.identity);
        _currentSonar.GetComponent<Sonar>().ActivateSonar(_childTransform.position, _sonarRange);
    }
    #endregion

    #region Upgrades
    public void UpgradeSubmarine(SubmarineUpgrades upgrade)
    {
        if (_money < upgrade._Cost) return;

        DecreaseMoney(upgrade._Cost);

        switch(upgrade._UpgradeType)
        {
            case SubmarineUpgrades.UpgradeType.HullUpgrade:
                _hullStrength += upgrade._HullUpgradeAmount;
                UpdateHull();
                Debug.Log($"Hull upgraded, status: {_hullStrength}");
                break;

            case SubmarineUpgrades.UpgradeType.OxygenUpgrade:
                _maxOxygen += upgrade._OxygenUpgradeAmount;
                UpdateOxygenBar();
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

    private void AddMoney()
    {
        _money += _moneyIncreaseAmount;
        UpdateMoney();
    }

    private void DecreaseMoney(int cost)
    {
        _money -= cost;
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        _moneyDisplay.text = $"Money: {_money}";
    }
    #endregion

    #region Collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            TakeDamage(10f);
        }

        if (collision.gameObject.tag == "Fish")
        {
            TakeDamage(5f);
        }

        if(collision.gameObject.tag == "Shark")
        {
            TakeDamage(30f);
        }
        
        if (collision.gameObject.tag == "Shop")
        {
            collision.gameObject.GetComponent<Shop>().SlideInPanel();
            _movement.Stop();
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

        if (collision.gameObject.tag == "OxygenRefill")
        {
            _refillingOxygen = true;
        }

        if (collision.gameObject.tag == "Treasure")
        {
            if(collision.GetComponent<Chest>().IsChestActivated())
            {
                Destroy(collision.gameObject);
                AddMoney();
            }
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
