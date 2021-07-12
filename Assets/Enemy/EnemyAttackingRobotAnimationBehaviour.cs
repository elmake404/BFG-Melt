using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateHandIK
{
    active,
    nonActive
}

public class EnemyAttackingRobotAnimationBehaviour : EnemyAnimationBehaviour
{
    public AnimationCurve handIK_EvaluateGrapgh;

    private Transform _targetTransform;
    private StateHandIK _stateHandIK = StateHandIK.nonActive;
    private float _mapHandIk = 0f;

    private void OnAnimatorIK()
    {
        RecalculateMapForHandIK();
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, handIK_EvaluateGrapgh.Evaluate(_mapHandIk));
        _animator.SetIKPosition(AvatarIKGoal.RightHand, _targetTransform.position);
    }

    public void SetTargetTransform(Transform target)
    {
        _targetTransform = target;
    }

    public void SetStateIKHand(StateHandIK state)
    {
        _stateHandIK = state;
    }

    private void RecalculateMapForHandIK()
    {
        switch (_stateHandIK)
        {
            case StateHandIK.active:
                _mapHandIk = _mapHandIk + 0.5f * Time.deltaTime;
                break;
            case StateHandIK.nonActive:
                _mapHandIk = _mapHandIk - Time.deltaTime;
                break;
        }
        _mapHandIk = Mathf.Clamp01(_mapHandIk);
    }
}

