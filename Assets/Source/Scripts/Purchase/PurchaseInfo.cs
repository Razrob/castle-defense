using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PurchaseInfo<TIdentifier, TPrice>
{
    [SerializeField] private TIdentifier _identifier;
    [SerializeField] private TPrice _price;

    public TIdentifier Identifier => _identifier;
    public TPrice Price => _price;
}
