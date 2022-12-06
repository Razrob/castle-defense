using System;

public abstract class ConstructionPreviewBase : ConstructionBase 
{
    public override ConstructionSkinBase ConstructionSkinBase => throw new NotImplementedException();
    protected override HealthBarBase _healthBarBase => null;
}
