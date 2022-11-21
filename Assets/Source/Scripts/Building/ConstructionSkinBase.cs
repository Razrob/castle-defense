using UnityEngine;

public abstract class ConstructionSkinBase : MonoBehaviour
{
    [SerializeField] private Pose _localPose;
    [SerializeField] private ConstructionLevel _constructionLevel;

    public ConstructionLevel ConstructionLevel => _constructionLevel;

    private void Awake()
    {
        transform.localPosition = _localPose.position;
        transform.localRotation = _localPose.rotation;
    }
}
