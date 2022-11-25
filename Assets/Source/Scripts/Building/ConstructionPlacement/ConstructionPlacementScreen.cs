using System;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionPlacementScreen : UIScreen
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _applyButton;
    [SerializeField] private GameObject _cellsPanelParent;
    
    private ConstructionPlacementChoiceCell[] _cells;

    public bool IsOpen => _cellsPanelParent.activeSelf;
    public ConstructionPlacementChoiceCell SelectedCell { get; private set; }

    public event Action<bool> OnActiveChange;
    public event Action OnConstructionSelect;
    public event Func<bool> OnTryApply;

    private void Start()
    {
        _openButton.onClick.AddListener(() => ChangePanelActive(true));    
        _closeButton.onClick.AddListener(() => ChangePanelActive(false));    
        _applyButton.onClick.AddListener(OnApplyButtonClick);

        _cells = GetComponentsInChildren<ConstructionPlacementChoiceCell>(true);

        foreach (ConstructionPlacementChoiceCell cell in _cells)
            cell.OnClick += OnConstructionPlacementCellClick;
    }

    private void OnConstructionPlacementCellClick(ConstructionPlacementChoiceCell cell)
    {
        SelectedCell = cell;
        OnConstructionSelect?.Invoke();
    }

    private void ChangePanelActive(bool value)
    {
        if (IsOpen == value)
            return;

        _cellsPanelParent.SetActive(value);
        OnActiveChange?.Invoke(value);
    }

    private void OnApplyButtonClick()
    {
        if (OnTryApply is null)
            return;

        if (OnTryApply())
            SelectedCell = null;
    }
}
