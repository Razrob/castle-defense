using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : CycleInitializerBase
{
    private UnitRepository _unitRepository;
    [SerializeField ]private GameObject _unit;

    protected override void OnInit()
    {
        _unitRepository = FWC.GlobalData.UnitRepository;
    }
    private void Update()
    {
        if (_unitRepository.Units.Count<1)
            Spawn();
    }

    public void Spawn()
    {
        GameObject unit = Instantiate(_unit,new Vector3(0,0,1), Quaternion.identity);
        _unitRepository.AddUnit(unit.GetComponent<WarrionAttackUnit>());
    }
}
