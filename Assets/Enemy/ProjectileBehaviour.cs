using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour
{
    private Transform _projectileTransform;
    private Transform _targetTransform;
    private EnemyAttackingRobotBehaviour _source;
    private float maxTwistedFactor = 1f;
    private Vector3 _launchedPos;
    private Vector3 _rotatedVector = Vector3.up;
    private Vector3 _traveledPoint;
    private ProjectileReceiver projectileReceiver;

    private float _minDistAtLaunchedPos = 2f;
    private float _minDistAtTarget = 4f;
    private float _minTravelSpdFactor = 0.3f;
    private float _maxTravelSpdFactor = 5f;

    private float projectileHP = 1f;


    public ProjectileBehaviour(GameObject projectile, Vector3 launchedPos, Transform target, EnemyAttackingRobotBehaviour enemyAttackingRobotBehaviour)
    {
        _launchedPos = launchedPos;
        _projectileTransform = GameObject.Instantiate(projectile).transform;
        _projectileTransform.gameObject.AddComponent<ProjectileReceiver>().SetDestinationProjectile(this);
        _projectileTransform.position = launchedPos;
        _targetTransform = target;
        _traveledPoint = launchedPos;
        _source = enemyAttackingRobotBehaviour;
    }

    public void UpdateProjectile()
    {
        float distanceMap = GetFloatMapDistBeetweenLaunchedPosToTarget();
        float lerpedTwistedFactor = Mathf.Lerp(0f, maxTwistedFactor, distanceMap);
        float lerpedTravelSpdFactor = Mathf.Lerp(_maxTravelSpdFactor, _minTravelSpdFactor, distanceMap);

        _traveledPoint = GetNewTraveledPoint(lerpedTravelSpdFactor);
        _rotatedVector = GetTwistedVectorProjectile();
        _projectileTransform.position = _traveledPoint + lerpedTwistedFactor * _rotatedVector;
    }

    public void ApplyDamageToProjectile()
    {
        projectileHP -= Time.deltaTime;

        if (projectileHP < 0)
        {
            _source.DeleteDestroyedProjectileFromList(this);
            DestroyProjectile();
        }

    }

    public Transform GetTransformProjectile()
    {
        return _projectileTransform;
    }

    private float GetFloatMapDistBeetweenLaunchedPosToTarget()
    {
        float distanceAtLaunchedPos = Vector3.Distance(_traveledPoint, _launchedPos);
        float distanceAtTarget = Vector3.Distance(_traveledPoint, _targetTransform.position);

        if (distanceAtLaunchedPos < _minDistAtLaunchedPos)
        {
            return Mathf.InverseLerp(0f, _minDistAtLaunchedPos, distanceAtLaunchedPos);
        }
        else if (distanceAtTarget < _minDistAtTarget)
        {
            return Mathf.InverseLerp(0f, _minDistAtTarget, distanceAtTarget);
        }
        else
        {
            return 1f;
        }

        
    }

    private Vector3 GetTwistedVectorProjectile()
    {
        Vector3 dirToTarget = _targetTransform.position - _projectileTransform.position;
        //Vector3 shortestPointOnVector = Vector3.Project(_rotatedVector, Vector3.Normalize(dirToTarget));
        Quaternion rotationOffset = Quaternion.AngleAxis(80f * Time.fixedDeltaTime, Vector3.Normalize(dirToTarget));
        return rotationOffset * _rotatedVector;
    }

    private Vector3 GetNewTraveledPoint(float factor)
    {
        return Vector3.MoveTowards(_traveledPoint, _targetTransform.position, factor * Time.fixedDeltaTime);
    }

    private void DestroyProjectile()
    {
        GameObject.Destroy(_projectileTransform.gameObject);
        
    }
}
