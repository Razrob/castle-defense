using System;

public class CollectResourceTriggerBehaviour : ProduceConstructionTriggerBehaviour 
{
    protected override Func<ITriggerable, bool> EnteredComponentIsSuitable => triggerable => triggerable is Player;
}