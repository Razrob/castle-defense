using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EaseExtensionsData", menuName = "Config/EaseExtensionsData")]
public class EaseExtensionsConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private EaseExtension[] _easeExtensions;

    public IReadOnlyDictionary<EaseExtensionType, EaseExtension> EaseExtensions { get; private set; }

    private void OnEnable()
    {
        EaseExtensions = _easeExtensions?.ToDictionary(extension => extension.EaseExtensionType, extension => extension);
    }
}
