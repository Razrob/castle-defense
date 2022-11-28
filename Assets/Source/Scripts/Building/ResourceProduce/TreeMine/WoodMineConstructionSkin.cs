using System;
using UnityEngine;

public class WoodMineConstructionSkin : ResourceProduceConstructionSkinBase
{
    [SerializeField] private GameObject[] _woods;

    public void SetVisualRepresentationScale(float fill)
    {
        int activeCount = Convert.ToInt32(_woods.Length * fill);

        for (int i = 0; i < _woods.Length; i++)
            _woods[i].SetActive(i < activeCount);
    }
}
