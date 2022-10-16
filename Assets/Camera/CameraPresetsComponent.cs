using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraPresetsComponent : MonoBehaviour
{
    [SerializeField] private CameraPreset[] _presets;
    public IReadOnlyDictionary<CameraPresetType, CameraPreset> Presets;

    private void Awake()
    {
        Presets = _presets.ToDictionary(preset => preset.CameraPresetType, preset => preset);
    }
}
