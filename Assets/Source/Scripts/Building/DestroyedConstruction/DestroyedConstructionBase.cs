using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class DestroyedConstructionBase : ConstructionBase
{
    [SerializeField] private Rigidbody[] _parts;

    private IReadOnlyList<DestroyedConstructionPart> _destroyedConstructionParts;
    private ConstructionDestroyInfo _currentDestroyInfo;

    public DestroyedConstructionState DestroyedConstructionState { get; private set; }
    public abstract DestroyedConstructionID DestroyedConstructionID { get; }

    public event Action<DestroyedConstructionBase> OnStateChange;

    private void Awake()
    {
        _destroyedConstructionParts = _parts.Select(part => new DestroyedConstructionPart
        {
            Rigidbody = part,
            Pose = new Pose(part.position, part.rotation),
            LocalScale = part.transform.localScale,
        }).ToArray();

        SetConstructionState(DestroyedConstructionState.Not_Destroyed);
    }

    public void Destroy(ConstructionDestroyInfo destroyInfo)
    {
        if (DestroyedConstructionState != DestroyedConstructionState.Not_Destroyed)
            return;

        _currentDestroyInfo = destroyInfo;

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(destroyInfo.DestroyDelay);
        sequence.AppendCallback(() => SetConstructionState(DestroyedConstructionState.Destroing_Process));
        sequence.AppendInterval(destroyInfo.DestroyDuration);

        foreach (DestroyedConstructionPart constructionPart in _destroyedConstructionParts)
        {
            sequence.AppendCallback(() =>
            {
                constructionPart.Rigidbody.transform.DOScale(0f, 3f)
                    .OnComplete(() => constructionPart.Rigidbody.gameObject.SetActive(false));
            });

            sequence.AppendInterval(destroyInfo.PartsLiquidateInterval);
        }

        sequence.AppendInterval(3f);

        sequence.AppendCallback(() => SetConstructionState(DestroyedConstructionState.Destroyed));
        sequence.Play();

        DestroyCallback();
    }

    protected virtual void DestroyCallback() { }

    private void SetConstructionState(DestroyedConstructionState destroyedConstructionState)
    {
        DestroyedConstructionState = destroyedConstructionState;

        if (destroyedConstructionState is DestroyedConstructionState.Destroyed)
            return;

        foreach (DestroyedConstructionPart part in _destroyedConstructionParts)
        {
            part.Rigidbody.useGravity = destroyedConstructionState is DestroyedConstructionState.Destroing_Process;
            part.Rigidbody.isKinematic = destroyedConstructionState != DestroyedConstructionState.Destroing_Process;

            if (destroyedConstructionState is DestroyedConstructionState.Not_Destroyed)
            {
                part.Rigidbody.gameObject.SetActive(true);
                part.Rigidbody.position = part.Pose.position;
                part.Rigidbody.rotation = part.Pose.rotation;
                part.Rigidbody.transform.localScale = part.LocalScale;
            }
            else if (destroyedConstructionState is DestroyedConstructionState.Destroing_Process)
            {
                part.Rigidbody.AddForce(_currentDestroyInfo.PartsVelocity, ForceMode.VelocityChange);
            }
        }

        OnStateChange?.Invoke(this);
    }

    public override void OnElementReturnCallback()
    {
        SetConstructionState(DestroyedConstructionState.Not_Destroyed);
    }
}