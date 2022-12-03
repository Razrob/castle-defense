using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class AttackIterationsSpawner : CycleInitializerBase
{
    [Inject] private AttackLinesConfig _attackLinesConfig;

    private LevelProgressData _levelProgressData => FWC.GlobalData.LevelProgressData;

    protected override void OnInit()
    {
        FWC.GlobalData.LevelProgressData.OnLevelStart += OnLevelStart;
    }

    private void OnLevelStart()
    {
        SpawnNextAttackIteration();
    }

    private void SpawnNextAttackIteration()
    {
        _levelProgressData.ActiveLevelInfo.IterationUnitsProcessor.OnActiveUnitsDied -= SpawnNextAttackIteration;

        int newAttackIterationIndex = _levelProgressData.ActiveLevelInfo.IterationUnitsProcessor.AttackIterationIndex + 1;

        if (_levelProgressData.ActiveLevelInfo.LevelConfig.AttackIterations.Count <= newAttackIterationIndex)
            return;

        AttackIteration attackIteration = _levelProgressData.ActiveLevelInfo.LevelConfig.AttackIterations[newAttackIterationIndex];
        Sequence sequence = DOTween.Sequence();

        foreach (AttackIteration.IterationPart iterationPart in attackIteration.Parts)
        {
            for (int unitIndex = 0; unitIndex < iterationPart.Count; unitIndex++)
            {
                sequence.AppendCallback(() => SpawnUnit(iterationPart.UnitID));
                sequence.AppendInterval(iterationPart.SpawnInterval);
            }

            sequence.AppendInterval(iterationPart.FinalDelay);
        }

        sequence.OnComplete(() =>
        {
            if (_levelProgressData.ActiveLevelInfo.IterationUnitsProcessor.ActiveUnitsDied())
                SpawnNextAttackIteration();
            else
                _levelProgressData.ActiveLevelInfo.IterationUnitsProcessor.OnActiveUnitsDied += SpawnNextAttackIteration;
        });
    }

    private void SpawnUnit(UnitID unitID)
    {
        Vector3 position = _attackLinesConfig.AttackLines[Random.Range(0, _attackLinesConfig.AttackLines.Count)].StartPosition;
        UnitBase unit = FWC.GlobalData.UnitsPool.ExtractElement(unitID);
        unit.transform.position = position;

        FWC.GlobalData.UnitRepository.AddUnit(unit);
    }
}
