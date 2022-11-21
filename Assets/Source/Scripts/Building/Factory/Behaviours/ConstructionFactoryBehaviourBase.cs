using UnityEngine;

public abstract class ConstructionFactoryBehaviourBase : MonoBehaviour
{
    protected void Awake()
    {
        OnInit();
    }

    protected virtual void OnInit() { }

    public abstract ConstructionType ConstructionType { get; }

    public abstract ConstructionConfiguration<IConstruction> GetConfiguration(ConstructionID constructionID);
    public abstract TConstruction CreateSolid<TConstruction>(ConstructionID constructionID, ConstructionLevel level) 
        where TConstruction : ConstructionBase;
    public abstract TSkin CreateSkin<TSkin>(ConstructionID constructionID, ConstructionLevel level)
        where TSkin : ConstructionSkinBase;
    public abstract TPreview CreatePreview<TPreview>(ConstructionID constructionID) where TPreview : ConstructionPreviewBase;
}
