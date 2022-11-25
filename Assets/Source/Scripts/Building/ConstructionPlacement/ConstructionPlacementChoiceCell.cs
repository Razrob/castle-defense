using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class ConstructionPlacementChoiceCell : MonoBehaviour
{
    [SerializeField] private ConstructionID _constructionID;
    [Inject] private ConstructionsPurchaseConfig _constructionsPurchaseConfig;

    private ResourceUICell[] _priceCells;
    private Button _button;

    public ConstructionID ConstructionID => _constructionID;
    public ResourceSpan Price => _constructionsPurchaseConfig.Prices[ConstructionID].Price;

    public event Action<ConstructionPlacementChoiceCell> OnClick;

    private void Awake()
    {
        _priceCells = GetComponentsInChildren<ResourceUICell>(true);
        _button = GetComponentInChildren<Button>(true);
        _button.onClick.AddListener(() => OnClick(this));

        RefreshPriceCells();
    }

    private void RefreshPriceCells()
    {
        foreach (ResourceUICell cell in _priceCells)
            cell.gameObject.SetActive(false);

        for (int i = 0; i < Price.Pairs.Count; i++)
        {
            ResourceSpan.Pair pair = Price.Pairs.Values.ElementAt(i);

            _priceCells[i].SetCount(pair.IntValue);
            _priceCells[i].SetIcon(FWC.GlobalData.ResourceRepository.Configs[pair.ResourceID].Icon);
            _priceCells[i].gameObject.SetActive(true);
        }
    }
}