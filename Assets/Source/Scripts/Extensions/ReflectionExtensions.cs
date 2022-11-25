using System;
using System.Collections.Generic;

public static class ReflectionExtensions
{
    public static Type[] GetHierarchyTypes(Type downType, bool reverse = false)
    {
        List<Type> types = new List<Type>();

        Type current = downType;

        while (true)
        {
            if (current is null)
                break;

            types.Add(current);
            current = current.BaseType;
        }

        if (reverse)
            types.Reverse();

        return types.ToArray();
    }
}