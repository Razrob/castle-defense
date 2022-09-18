using UnityEngine;

public class TestInitializer : CycleInitializerBase
{
    protected override void OnInit()
    {
        Debug.Log("OnInit");
    }

    protected override void OnUpdate()
    {
        Debug.Log("OnUpdate");
    }

    protected override void OnFixedUpdate()
    {
        Debug.Log("OnFixedUpdate");
    }
}
