using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ConstructionPlacementChoiceCell : MonoBehaviour
{
    [SerializeField] private ConstructionID _constructionID;

    private Button _button;

    public ConstructionID ConstructionID => _constructionID;

    public event Action<ConstructionPlacementChoiceCell> OnClick;

    private void Awake()
    {
        _button = GetComponentInChildren<Button>(true);
        _button.onClick.AddListener(() => OnClick(this));
    }
}