using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct ResourceSpan : ISerializationCallbackReceiver
{
    [SerializeField] private Pair[] _pairs;

    public IReadOnlyDictionary<ResourceID, Pair> Pairs { get; private set; }

    public ResourceSpan(IEnumerable<Pair> pairs) : this()
    {
        _pairs = pairs.ToArray();
        InitPairs();
    }

    private void InitPairs()
    {
        try
        {
            Pairs = _pairs?.ToDictionary(p => p.ResourceID, p => p);
        }
        catch(Exception e) 
        {
            Debug.Log(e.Message);
        }
    }

    public void OnBeforeSerialize() { }
    public void OnAfterDeserialize() => InitPairs();


    [Serializable]
    public struct Pair
    {
        [SerializeField] private ResourceID _resourceID;
        [SerializeField] private float _value;

        public Pair(ResourceID resourceID, float value)
        {
            _resourceID = resourceID;
            _value = value;
        }

        public ResourceID ResourceID => _resourceID;
        public float Value => _value;
        public int IntValue => Convert.ToInt32(_value);
    }
}