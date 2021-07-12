using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    public SerializeStructRobotBreakPoints serializeStructRobotBreakPoints;

    public EnemyAttackingRobotAnimationBehaviour enemyRobotAnimationBehaviour;
    public Transform rightWristJoint;

    protected PlayerBehaviour _playerBehaviour;
    protected Transform _thisTransform;
    protected Transform _playerTransform;

    protected RobotDestructionPointMap _robotDestructionPointMap;

    protected virtual void Start()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    protected void MoveToTarget()
    {
        Vector3 newPos = Vector3.MoveTowards(_thisTransform.position, _playerTransform.position, 0.1f * Time.fixedDeltaTime);
        newPos.y = 0f;
        _thisTransform.position = newPos;
    }

    protected void RotateToTarget()
    {
        Vector3 dirToPlayer = _playerTransform.position - _thisTransform.position;
        Quaternion newRotation = Quaternion.RotateTowards(_thisTransform.rotation, Quaternion.Euler(0f, Quaternion.LookRotation(dirToPlayer).eulerAngles.y, 0f), 7f * Time.fixedDeltaTime);
        _thisTransform.rotation = newRotation;
    }

    public void SetPlayerBehaviour(PlayerBehaviour playerBehaviour)
    {
        _playerBehaviour = playerBehaviour;
        _playerTransform = playerBehaviour.transform;
        enemyRobotAnimationBehaviour.SetTargetTransform(playerBehaviour.transform);
    }
}
