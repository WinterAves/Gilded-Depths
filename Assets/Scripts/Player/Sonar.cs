using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    [SerializeField] private AudioSource _blip;

    [SerializeField] private float _expansionSpeed;
    [SerializeField] private Vector3 _offset;
    private float _expansionLimit;
    private float _expansion = 0f;
    private bool _expand = false;

    void Update()
    {
        ExpandSonar();
    }

    public void ActivateSonar(Vector3 position, float expansionLimit)
    {
        _blip.Play();
        transform.position = position + _offset;
        _expansionLimit = expansionLimit;
        _expand = true;

    }

    private void ExpandSonar()
    {
        if (!_expand) return;

        _expansion += Time.deltaTime * _expansionSpeed;
        transform.localScale = new Vector3(_expansion, _expansion, 1f);

        if (_expansion >= _expansionLimit)
            Destroy(gameObject);
    }
}
