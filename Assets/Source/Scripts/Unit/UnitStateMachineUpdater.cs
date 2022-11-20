using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachineUpdater : CycleInitializerBase
{
    private UnitRepository _unitRepository;

    protected override void OnInit()
    {
        _unitRepository = FWC.GlobalData.UnitRepository;
        foreach (KeyValuePair<UnitType, List<UnitBase>> unitList in _unitRepository.Units)
        {
            foreach (UnitBase unit in unitList.Value)
            {
             //   unit.Awake();
            }
        }
    }

    protected override void OnUpdate()
    {
        foreach (KeyValuePair<UnitType, List<UnitBase>> unitList in _unitRepository.Units)
        {
            foreach (UnitBase unit in unitList.Value)
            {
        //        unit.OnUpdateMain();
            }
        }
    }
    protected override void OnFixedUpdate()
    {
        foreach (KeyValuePair<UnitType, List<UnitBase>> unitList in _unitRepository.Units)
        {
            foreach (UnitBase unit in unitList.Value)
            {
            //    unit.OnFixedUpdateMain();
            }
        }
    }
}
