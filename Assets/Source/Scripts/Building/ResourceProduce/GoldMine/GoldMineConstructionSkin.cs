using UnityEngine;

public class GoldMineConstructionSkin : ResourceProduceConstructionSkinBase
{
    [SerializeField] private GameObject _goldVisualRepresentation;

    public void SetVisualRepresentationScale(float fill)
    {
        _goldVisualRepresentation.SetActive(fill >= 0.01f);
        _goldVisualRepresentation.transform.localScale = Vector3.one.SetZ(fill);
    }
}
