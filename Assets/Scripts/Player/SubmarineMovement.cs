using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineMovement : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _force = 800f;
    [SerializeField] private float _accelerationSpeed = 2f;
    [SerializeField] private float _decelerationSpeed = 2f;
    [SerializeField] private float _rotationSpeed = 3f;

    private Rigidbody2D _rigidBody;
    private bool _accelerating = false;
    private bool _shopping = false;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(!_shopping)
        {
            Move();
            Decelerate();
            Rotate();
        }
    }

    private void Move()
    {
        Vector3 mousePos = Vector3.zero;

        _accelerating = false;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = new Vector2(mousePos.x, mousePos.y) - _rigidBody.position;
            direction = direction.normalized;

            _rigidBody.AddForce(direction * _force * Time.deltaTime, ForceMode2D.Force);
            
            if(_rigidBody.velocity.magnitude > _maxSpeed)
            {
                _rigidBody.velocity = _rigidBody.velocity.normalized * _maxSpeed;
            }

            _accelerating = true;
            Debug.DrawRay(transform.position, direction * 5f, Color.red);
        }
    }

    private void Decelerate()
    {
        if(!_accelerating)
        {
            float reducedMagnitude = _rigidBody.velocity.magnitude;
            reducedMagnitude = Mathf.Clamp(reducedMagnitude - _decelerationSpeed * Time.deltaTime, 0f, _maxSpeed);
            _rigidBody.velocity = _rigidBody.velocity.normalized * reducedMagnitude;
        }
    }

    private void Rotate()
    {
        if (_rigidBody.velocity.x > 0f)
        {
            transform.localEulerAngles = new Vector3(0f, Mathf.Clamp(transform.eulerAngles.y - _rotationSpeed * Time.deltaTime, 0f, 180f), 0f);
        }
        else if (_rigidBody.velocity.x < 0f)
        {
            transform.localEulerAngles = new Vector3(0f, Mathf.Clamp(transform.eulerAngles.y + _rotationSpeed * Time.deltaTime, 0f, 180f), 0f);
        }
    }

    public float UpgradeSpeed(float upgrade)
    {
        _maxSpeed += upgrade;
        _accelerationSpeed += upgrade * 0.4f;
        return _maxSpeed;
    }

    #region Shopping
    public void SetShopping()
    {
        _shopping = true;
    }

    public void UnsetShopping()
    {
        _shopping = false;
    }

    public void Stop()
    {
        _rigidBody.velocity = Vector3.zero;
    }
    #endregion
}
