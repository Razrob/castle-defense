using UnityEngine;

public abstract class BuildingProcessSkinBase : ConstructionBase, IConstruction
{
    [SerializeField] private ConstructionLevel _constructionLevel;

    public ConstructionLevel ConstructionLevel => _constructionLevel;
}
