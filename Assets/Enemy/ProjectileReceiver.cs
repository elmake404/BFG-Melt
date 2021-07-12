using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileReceiver : MonoBehaviour
{
    private ProjectileBehaviour _destinationProjectile;

    public void SetDestinationProjectile(ProjectileBehaviour projectileBehaviour)
    {
        _destinationProjectile = projectileBehaviour;
    }

    public void SendStepHit()
    {
        _destinationProjectile.ApplyDamageToProjectile();
    }

    
}
