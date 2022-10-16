using UnityEngine;
using System;
using Cinemachine;

[Serializable]
public class CameraPreset
{
    [SerializeField] private CameraPresetType _presetType;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    public CameraPresetType CameraPresetType => _presetType;
    public CinemachineVirtualCamera CinemachineVirtualCamera => _virtualCamera;
}
