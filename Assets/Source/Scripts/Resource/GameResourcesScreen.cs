using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameResourcesScreen : UIScreen
{
    [SerializeField] private ResourceUICell[] _resourcesCells;

    private IReadOnlyDictionary<ResourceID, ResourceUICell> _cells;

    private void Awake()
    {
        _cells = _resourcesCells.ToDictionary(c => c.ResourceID, c => c);
    }

    public void SetResourceCount(ResourceID resourceID, int count)
    {
        if (!_cells.ContainsKey(resourceID))
            return;

        _cells[resourceID].SetCount(count);
    }
}