using Cinemachine;
using System;
using UnityEngine;

public class CameraPresetsChangeData
{
    private Camera _camera;
    private CinemachineBrain _cinemachineBrain;
    private CameraPresetsComponent _cameraPresetsComponent;

    public CameraPresetsChangeData()
    {
        _camera = Camera.main;
        _cinemachineBrain = _camera.GetComponent<CinemachineBrain>();
        _cameraPresetsComponent = UnityEngine.Object.FindObjectOfType<CameraPresetsComponent>();
    }

    public void SetCameraTarget(CameraPresetType cameraPresetType, Transform target)
    {
        _cameraPresetsComponent.Presets[cameraPresetType].CinemachineVirtualCamera.Follow = target;
    }

    public void ChangeCameraPresetPriority(CameraPresetType cameraPresetType, int offcet)
    {
        _cameraPresetsComponent.Presets[cameraPresetType].CinemachineVirtualCamera.Priority += offcet;
    }
}