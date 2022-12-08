using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AttackUnitsMoveState : UnitMoveStateBase
{
    private EntityStateID _entityStateID = EntityStateID.Move;
    public override EntityStateID EntityStateID => _entityStateID;

    private Animator _animator;
    private Vector3 _velocity => _unit.Velocity;
    private AttackUnit _unit;
    private ConstructionsRepository _constructionsRepository;

    private CostructionPathInfo _costructionPathInfo;
    public CostructionPathInfo CostructionPathInfo => _costructionPathInfo;

    private const int MOVE_LAYER_INDEX = 0;

    public AttackUnitsMoveState(AttackUnit attackUnit) : base(attackUnit.Animator) 
    {
        _unit = attackUnit;
        _animator = attackUnit.Animator;
    }

    public override void OnStateEnter()
    {
        _animator.SetLayerWeight(MOVE_LAYER_INDEX, 1f);
        _constructionsRepository = FWC.GlobalData.ConstructionsRepository;
    }

    public override void OnStateExit()
    {
        _animator.SetLayerWeight(MOVE_LAYER_INDEX, 0f);
    }

    public override void OnUpdate() { }

    public override void OnFixedUpdate()
    {
        FindTarget();
        _animator.SetFloat("MoveSpeed", _velocity.magnitude);
    }

    private void FindTarget()
    {
        CostructionPathInfo pathInfo = GetNearestPathToConstruction<AttackConstruction>(_unit, _constructionsRepository);
        
        if (pathInfo.Path.status == NavMeshPathStatus.PathComplete)
        {
            RefreshPathInfo(pathInfo);
        }
        else
        {
            pathInfo = GetNearestPathToConstruction<DefenceWallConstruction>(_unit, _constructionsRepository);
            if (pathInfo.Path.status == NavMeshPathStatus.PathComplete)
                RefreshPathInfo(pathInfo);
        }
    }

    private void RefreshPathInfo(CostructionPathInfo pathInfo)
    {
        _unit.NavMeshAgent.path = pathInfo.Path;
        _costructionPathInfo = pathInfo;
    }
}

public class AnimatorStateMachine
{
    private AnimatorState _activeState;
    private Animator _animator;
    private readonly Dictionary<AnimatorState, AnimatorStateBase> _states;

    public AnimatorState ActiveState => _activeState;

    public AnimatorStateMachine(Animator animator, IEnumerable<AnimatorStateBase> states, AnimatorState activeState)
    {
        _states = states.ToDictionary(state => state.State, state => state);
        _activeState = activeState;
        _animator = animator;
    }

    public void SetState(AnimatorState state)
    {
        _activeState = state;
        _states[_activeState].SetActive(_animator);
    }

    public void SetState(AnimatorState state, int numberAnimation)
    {
        _activeState = state;
        _states[_activeState].SetActive(_animator, numberAnimation);
    }
}
 
public class AnimatorStateBase
{
    private AnimatorState _state;
    private int _countAnimations;
    private int _layer;

    public int CountAnimations => _countAnimations;
    public AnimatorState State => _state;

    public AnimatorStateBase(AnimatorState state, int countAnimations, int layer)
    {
        _state = state;
        _countAnimations = countAnimations;
        _layer = layer;
    }

    public void SetActive(Animator animator)
    {
        animator.SetLayerWeight(_layer, 1);
    }

    public void SetActive(Animator animator, int numberAnimation)
    {
        animator.SetFloat("AttackType", numberAnimation);
        animator.SetLayerWeight(_layer, 1);
    }
}
