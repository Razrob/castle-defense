using System;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionPlacementScreen : UIScreen
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _applyButton;
    [SerializeField] private GameObject _cellsPanelParent;
    [SerializeField] private ConstructionPlacementChoiceCell[] _cells;

    public bool IsOpen => _cellsPanelParent.activeSelf;
    public ConstructionID SelectedConstructionID { get; private set; } = ConstructionID.Defence_Wall;

    public event Action<bool> OnActiveChange;
    public event Action OnConstructionSelect;
    public event Action OnApply;

    private void Start()
    {
        _openButton.onClick.AddListener(() => ChangePanelActive(true));    
        _closeButton.onClick.AddListener(() => ChangePanelActive(false));    
        _applyButton.onClick.AddListener(OnApplyButtonClick);

        foreach (ConstructionPlacementChoiceCell cell in _cells)
            cell.OnClick += OnConstructionPlacementCellClick;
    }

    private void OnConstructionPlacementCellClick(ConstructionPlacementChoiceCell cell)
    {
        SelectedConstructionID = cell.ConstructionID;
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
        OnApply?.Invoke();
    }
}
