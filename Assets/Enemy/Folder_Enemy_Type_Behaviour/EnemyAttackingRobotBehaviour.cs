using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingRobotBehaviour : EnemyBehaviour
{
    

    public GameObject projectile;
    public GameObject onDestroyProjectileParticles;
    public GameObject muzzleParticles;
    public GameObject prepareShootParticles;

    private Coroutine _periodicShootCoroutina;
    private GameObject _instancedMuzzleParticles;
    private ParticleSystem _particlesSystemMuzzleParticles;
    private GameObject _instancedPrepareShootParticles;
    private ParticleSystem _particlesSystemPrepareShootParticles;
    private List<ProjectileBehaviour> _launchedProjectileBehaviours;

    protected override void Start()
    {
        _robotDestructionPointMap = new RobotDestructionPointMap(serializeStructRobotBreakPoints);
        _launchedProjectileBehaviours = new List<ProjectileBehaviour>();
        _thisTransform = transform;
        InitMuzzleParticles();
        InitPrepareShootParticles();
        //_periodicShootCoroutina = StartCoroutine(PeriodicShoot());
        
    }

    protected override void FixedUpdate()
    {
        base.MoveToTarget();
        base.RotateToTarget();
        UpdateLaunchedProjectiles();
    }

    public void DeleteDestroyedProjectileFromList(ProjectileBehaviour projectileBehaviour)
    {
        
        int destroyedHash = projectileBehaviour.GetHashCode();
        int desiredIndex = _launchedProjectileBehaviours.FindIndex(x => x.GetHashCode() == destroyedHash);
        InstantiateUtilities.InstantiateAndDestroydAfterTime(onDestroyProjectileParticles, _launchedProjectileBehaviours[desiredIndex].GetTransformProjectile().position, 1.5f, typeof(HelpMonoBehaviour));
        _launchedProjectileBehaviours.RemoveAt(desiredIndex);

    }


    private void InitMuzzleParticles()
    {
        _instancedMuzzleParticles = Instantiate(muzzleParticles, rightWristJoint);
        _instancedMuzzleParticles.transform.rotation = Quaternion.LookRotation(rightWristJoint.transform.right);
        _particlesSystemMuzzleParticles = _instancedMuzzleParticles.GetComponent<ParticleSystem>();
        
    }

    private void InitPrepareShootParticles()
    {
        _instancedPrepareShootParticles = Instantiate(prepareShootParticles, rightWristJoint);
        _particlesSystemPrepareShootParticles = _instancedPrepareShootParticles.GetComponent<ParticleSystem>();
    }

    private void PlayMuzzleParticles()
    {
        _particlesSystemMuzzleParticles.Play(true);
    }

    private void PlayPrepareShootParticles()
    {
        _particlesSystemPrepareShootParticles.Play(true);
    }

    private void StopPrepareShootParticles()
    {
        _particlesSystemPrepareShootParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    private void LaunnchNewProjectile()
    {
        ProjectileBehaviour projectileBehaviour = new ProjectileBehaviour(projectile, rightWristJoint.position, _playerTransform, this);
        _launchedProjectileBehaviours.Add(projectileBehaviour);
    }



    private void UpdateLaunchedProjectiles()
    {
        for (int i = 0; i < _launchedProjectileBehaviours.Count; i++)
        {
            _launchedProjectileBehaviours[i].UpdateProjectile();
        }
    }

    private IEnumerator PeriodicShoot()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 5; i++)
        {
            enemyRobotAnimationBehaviour.SetStateIKHand(StateHandIK.active);
            PlayPrepareShootParticles();
            yield return new WaitForSeconds(2f);
            StopPrepareShootParticles();
            PlayMuzzleParticles();
            LaunnchNewProjectile();
            enemyRobotAnimationBehaviour.SetStateIKHand(StateHandIK.nonActive);
            yield return new WaitForSeconds(8f);
        }

        yield return null;
    }
}
