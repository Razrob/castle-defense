using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DestroyedConstructionConfig", menuName = "Config/Construction/DestroyedConstructionConfig")]
public class DestroyedConstructionConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private ConstructionConfiguration<DestroyedConstructionBase>[] _destroyedConstructions;

    private IReadOnlyDictionary<ConstructionID, ConstructionConfiguration<DestroyedConstructionBase>> _constructions;

    private void OnEnable()
    {
        try
        {
            _constructions = _destroyedConstructions?.ToDictionary(c => c.ConstructionID, c => c);
        }
        catch { }
    }

    public ConstructionConfiguration<DestroyedConstructionBase> GetConfiguration(ConstructionID constructionID)
    {
        if (!_constructions.ContainsKey(constructionID))
            throw new KeyNotFoundException($"Construction \"{constructionID}\" not found");

        return _constructions[constructionID];
    }
}