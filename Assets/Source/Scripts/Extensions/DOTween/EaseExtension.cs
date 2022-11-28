using System;
using UnityEngine;

[Serializable]
public class EaseExtension
{
    [SerializeField] private EaseExtensionType _easeExtensionType;
    [SerializeField] private AnimationCurve _curve;

    public EaseExtensionType EaseExtensionType => _easeExtensionType;
    public AnimationCurve Curve => _curve;
}
