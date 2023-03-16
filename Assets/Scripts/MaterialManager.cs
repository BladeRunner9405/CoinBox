using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialManager : MonoBehaviour
{

    [SerializeField] private Material _material;
    [SerializeField] private Image _defaultImageColor;
    public Color color;

    private void Start()
    {
        color = _defaultImageColor.color;
        PaintModel();
    }
    public void SetColor(Image imageColor)
    {
        color = imageColor.color;
        PaintModel();
    }

    public void PaintModel()
    {
        _material.color = color;
    }
}
