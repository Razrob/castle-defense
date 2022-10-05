using UnityEngine;
using Zenject;

public class ConfigsInstaller : MonoInstaller
{
    [SerializeField] private ConfigsRepository _configsRepository;

    public override void InstallBindings()
    {
        Container.BindInstance(_configsRepository).AsSingle();
    }
}