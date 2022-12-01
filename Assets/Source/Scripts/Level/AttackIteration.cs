using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackIteration
{
    [SerializeField] private IterationPart[] _parts;

    public IReadOnlyList<IterationPart> Parts => _parts;


    [Serializable]
    public struct IterationPart : ISerializationCallbackReceiver
    {
        public UnitID UnitID;
        public int Count;
        public float SpawnInterval;
        public float FinalDelay;

        public void OnAfterDeserialize()
        {
            SpawnInterval = Mathf.Max(SpawnInterval, 0.01f);
        }

        public void OnBeforeSerialize() { }
    }
}