using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using static Cinemachine.DocumentationSortingAttribute;

public interface IConstructionFactory
{
    public ConstructionConfiguration<IConstruction> GetConfiguration(ConstructionID constructionID);
    public TConstruction CreateSolid<TConstruction>(ConstructionID constructionID, ConstructionLevel level = ConstructionLevel.Level_1) 
        where TConstruction : ConstructionBase;
    public TSkin CreateSkin<TSkin>(ConstructionID constructionID, ConstructionLevel level = ConstructionLevel.Level_1)
        where TSkin : ConstructionSkinBase;
    public TPreview CreatePreview<TPreview>(ConstructionID constructionID) where TPreview : ConstructionPreviewBase;
}

public class ConstructionFactory : MonoBehaviour, IConstructionFactory
{
    [Inject] private readonly ConstructionTypeMatchConfig _constructionTypeMatchConfig;

    private IReadOnlyDictionary<ConstructionType, ConstructionFactoryBehaviourBase> _behaviours;

    private void Awake()
    {
        _behaviours = GetComponentsInChildren<ConstructionFactoryBehaviourBase>(true)
            .ToDictionary(behaviour => behaviour.ConstructionType, behaviour => behaviour);

        foreach (ConstructionFactoryBehaviourBase behaviour in _behaviours.Values)
            Debug.Log($"Factory behaviour {behaviour.GetType()} has been registered");
    }

    private void ThrowNotFoundException(ConstructionID constructionID)
    {
        throw new InvalidOperationException($"{constructionID} cannot be created, " +
            $"because factory for this construction not found. Create new factory behavoiur for this construction");
    }

    public ConstructionConfiguration<IConstruction> GetConfiguration(ConstructionID constructionID)
    {
        ConstructionType constructionType = _constructionTypeMatchConfig.GetConstructionType(constructionID);

        if (!_behaviours.ContainsKey(constructionType))
            ThrowNotFoundException(constructionID);

        return _behaviours[constructionType].GetConfiguration(constructionID);
    }

    public TConstruction CreateSolid<TConstruction>(ConstructionID constructionID, ConstructionLevel level = ConstructionLevel.Level_1)
        where TConstruction : ConstructionBase
    {
        ConstructionType constructionType = _constructionTypeMatchConfig.GetConstructionType(constructionID);

        if (!_behaviours.ContainsKey(constructionType))
            ThrowNotFoundException(constructionID);

        return _behaviours[constructionType].CreateSolid<TConstruction>(constructionID, level);
    }

    public TSkin CreateSkin<TSkin>(ConstructionID constructionID, ConstructionLevel level = ConstructionLevel.Level_1)
        where TSkin : ConstructionSkinBase
    {
        ConstructionType constructionType = _constructionTypeMatchConfig.GetConstructionType(constructionID);

        if (!_behaviours.ContainsKey(constructionType))
            ThrowNotFoundException(constructionID);

        return _behaviours[constructionType].CreateSkin<TSkin>(constructionID, level);
    }

    public TPreview CreatePreview<TPreview>(ConstructionID constructionID) where TPreview : ConstructionPreviewBase
    {
        ConstructionType constructionType = _constructionTypeMatchConfig.GetConstructionType(constructionID);

        if (!_behaviours.ContainsKey(constructionType))
            ThrowNotFoundException(constructionID);

        return _behaviours[constructionType].CreatePreview<TPreview>(constructionID);
    }
}
