using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Image _panel;

    private Animator _animator;
    private bool _panelIn = false;
    

    private void Awake()
    {
        _animator = _panel.GetComponent<Animator>();
    }

    public void SlideInPanel()
    {
        _animator.SetTrigger("SlideIn");
        _panelIn = true;
    }

    public void SlideOutPanel()
    {
        _animator.SetTrigger("SlideOut");
        _panelIn = false;
    }
}
