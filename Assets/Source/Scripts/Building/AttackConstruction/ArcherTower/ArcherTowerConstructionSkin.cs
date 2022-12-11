using UnityEngine;

public class ArcherTowerConstructionSkin : ConstructionSkinBase
{
    [SerializeField] private Animator _archerAnimator;
    [SerializeField] private Transform _rowSpawnPoint;
    [SerializeField] private Transform _archerRightHand;
    [SerializeField] private Transform _bowRopePointUp;
    [SerializeField] private Transform _bowRopePointDown;
    [SerializeField] private LineRenderer _rope;

    public Animator ArcherAnimator => _archerAnimator;
    public Transform RowSpawnPoint => _rowSpawnPoint;
    public Transform ArcherRightHand => _archerRightHand;
    public Transform BowRopePointUp => _bowRopePointUp;
    public Transform BowRopePointDown => _bowRopePointDown;
    public LineRenderer Rope => _rope;
}