using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    [SerializeField] private Transform _submarine;
    [SerializeField] private Vector2 _offset;

    private void Update()
    {
        transform.position = (Vector2)_submarine.position + _offset;
    }
}
