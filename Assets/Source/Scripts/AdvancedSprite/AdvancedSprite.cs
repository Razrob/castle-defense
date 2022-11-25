using UnityEngine;
using System;
using System.Collections.Generic;

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public sealed class AdvancedSprite : MonoBehaviour 
{
    [SerializeField] private bool _inverse = false;
    [SerializeField] [Range(0f, 1f)] private float _saturation = 1f;
    [SerializeField] [Range(0f, 1f)] private float _offcet = 0f;
    [SerializeField] private FillType _fillType = FillType.Radial;
    [SerializeField] [Range(0f, 1f)] private float _fill = 1f;

    private SpriteRenderer _spriteRenderer;

    private const string INVERSE = "_Inverse";
    private const string SATURATION = "_Saturation";
    private const string OFFCET = "_Offcet";

    private readonly Dictionary<FillType, string> _fillParameters = new Dictionary<FillType, string>()
    {
        { FillType.Radial,  "_RadialFill"},
        { FillType.Vertical,  "_VerticalFill"},
        { FillType.Horizontal,  "_HorizontalFill"},
    };

    public bool Inverse 
    { 
        get => _inverse;
        set
        {
            _inverse = value;
            RefreshShaderParameters();
        }
    }

    public float Saturation 
    { 
        get => _saturation;
        set
        {
            _saturation = Mathf.Clamp01(value);
            RefreshShaderParameters();
        }
    }

    public float Offcet 
    { 
        get => _offcet;
        set
        {
            _offcet = Mathf.Clamp01(value);
            RefreshShaderParameters();
        }
    }

    public float Fill 
    { 
        get => _fill;
        set
        {
            _fill = Mathf.Clamp01(value);
            RefreshShaderParameters();
        }
    }

    public FillType FillType 
    { 
        get => _fillType;
        set
        {
            _fillType = value;
            RefreshShaderParameters();
        }
    }

    private void Awake() 
    {
        CheckSpriteRenderer();
        RefreshShaderParameters();
    }

    private void OnValidate() 
    {
        RefreshShaderParameters();   
    }

    private void CheckSpriteRenderer()
    {
        if (_spriteRenderer is null)
            _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void RefreshShaderParameters()
    {
        CheckSpriteRenderer();

        if (!_spriteRenderer)
            return;

        Material material = Application.isPlaying ? _spriteRenderer.material : _spriteRenderer.sharedMaterial;

        material.SetInt(INVERSE, Convert.ToInt32(_inverse));
        material.SetFloat(SATURATION, _saturation);
        material.SetFloat(OFFCET, _offcet);

        foreach (FillType fillType in _fillParameters.Keys)
            material.SetFloat(_fillParameters[fillType], fillType == _fillType ? _fill : 1f);
    }
}
