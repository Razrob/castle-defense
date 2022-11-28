using DG.Tweening;
using System;
using UnityEngine;

public class WoodMineConstructionSkin : ResourceProduceConstructionSkinBase
{
    [SerializeField] private GameObject[] _woods;

    private int _lastActiveWoodsCount;

    public void SetVisualRepresentationScale(float fill)
    {
        int activeCount = Convert.ToInt32(_woods.Length * fill);

        for (int i = 0; i < _woods.Length; i++)
        {
            _woods[i].SetActive(i < activeCount);

            if (activeCount > _lastActiveWoodsCount && activeCount - 1 == i)
            {
                Vector3 scale = _woods[i].transform.localScale;
                _woods[i].transform.localScale = Vector3.zero;
                _woods[i].transform.DOScale(scale, 0.3f)
                    .SetEase(ConfigsRepository.FindConfig<EaseExtensionsConfig>()
                    .EaseExtensions[EaseExtensionType.EaseInCubicOutBack].Curve);
            }
        }

        _lastActiveWoodsCount = activeCount;
    }
}
