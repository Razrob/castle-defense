using System;
using UnityEngine;

[Serializable]
public struct SideIdentifier : ISideIdentifier
{
    [SerializeField] private string _identifier;

    public string Identifier => string.IsNullOrEmpty(_identifier) ? Unknown : _identifier;

    public static readonly string Unknown = "Unknown";
    public static readonly string NonComparable = "NonComparable";
    public static readonly string Suitable = "Suitable";

    public SideIdentifier(string identifier)
    {
        _identifier = identifier;
    }

    public bool Compare(ISideIdentifier sideIdentifier) => _identifier == sideIdentifier.Identifier;

    public override string ToString()
    {
        return _identifier;
    }
}