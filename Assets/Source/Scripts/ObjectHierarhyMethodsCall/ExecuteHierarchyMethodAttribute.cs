using System;

[AttributeUsage(AttributeTargets.Method)]
public class ExecuteHierarchyMethodAttribute : Attribute
{
    public readonly HierarchyMethodType MethodType;

    public ExecuteHierarchyMethodAttribute(HierarchyMethodType methodType)
    {
        MethodType = methodType;
    }
}
