using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class ObjectHierarchyMethodsExecutor
{
    private IReadOnlyDictionary<Type, ClassExecuteMethodsInfo> _hierarchyClassesMethods;

    public ObjectHierarchyMethodsExecutor(object baseObject)
    {
        Dictionary<Type, ClassExecuteMethodsInfo> hierarchyClassesMethods = new Dictionary<Type, ClassExecuteMethodsInfo>();

        Type[] hierarchy = ReflectionExtensions.GetHierarchyTypes(baseObject.GetType(), true);

        foreach (Type type in hierarchy)
        {
            Dictionary<HierarchyMethodType, Action> pairs = new Dictionary<HierarchyMethodType, Action>();

            MethodInfo[] methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .Where(method => method.GetCustomAttribute<ExecuteHierarchyMethodAttribute>() != null)
                .ToArray();

            foreach (MethodInfo methodInfo in methods)
            {
                ExecuteHierarchyMethodAttribute attribute = methodInfo.GetCustomAttribute<ExecuteHierarchyMethodAttribute>();

                if (!pairs.ContainsKey(attribute.MethodType))
                    pairs.Add(attribute.MethodType, methodInfo.CreateDelegate(typeof(Action), baseObject).Cast<Action>());
            }

            hierarchyClassesMethods.Add(type, new ClassExecuteMethodsInfo(pairs));
        }

        _hierarchyClassesMethods = hierarchyClassesMethods;
    }

    public void Execute(HierarchyMethodType methodType)
    {
        foreach (ClassExecuteMethodsInfo info in _hierarchyClassesMethods.Values)
            if (info.Methods.TryGetValue(methodType, out Action method))
                method();
    }


    private class ClassExecuteMethodsInfo
    {
        public IReadOnlyDictionary<HierarchyMethodType, Action> Methods { get; private set; }

        public ClassExecuteMethodsInfo(IReadOnlyDictionary<HierarchyMethodType, Action> methods)
        {
            Methods = methods;
        }
    }
}
