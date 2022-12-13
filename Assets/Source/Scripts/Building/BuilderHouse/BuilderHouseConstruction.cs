using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderHouseConstruction : ConstructionBase
{
    [SerializeField] private BuilderHouseConstructionSkin _skin;
    [SerializeField] private DefaultHealthBar _defaultHealth;

    public override ConstructionID ConstructionID => ConstructionID.Builder_House;
    public override ConstructionID DestroyCaseConstructionID => ConstructionID.Builder_House_Destroyed_Construction;
    public override ConstructionSkinBase ConstructionSkinBase => _skin;
    protected override HealthBarBase _healthBarBase => _defaultHealth;
}
