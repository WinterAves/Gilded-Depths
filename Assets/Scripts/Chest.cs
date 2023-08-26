using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Renderer _chestRenderer;
    [SerializeField] private Renderer _coinRenderer;

    private Material _chestMaterial;
    private Material _coinMaterial;

    private bool _activated = false;

    private void Awake()
    {
        _chestMaterial = _chestRenderer.material;
        _coinMaterial = _coinRenderer.material;
    }

    public void ActivateChest()
    {
        if (_activated) return;

        Debug.Log("Chest Detected");
        Color c = _chestMaterial.color;
        _chestMaterial.color = new Color(c.r, c.g, c.b, 1f);
        _chestMaterial.EnableKeyword("_EMISSION");

        c = _coinMaterial.color;
        _coinMaterial.color = new Color(c.r, c.g, c.b, 1f);
        _coinMaterial.EnableKeyword("_EMISSION");

        _activated = true;
    }

    public bool IsChestActivated()
    {
        return _activated;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Yes");
    }
}
