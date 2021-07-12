using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LayersID
{
    SDF = 9,
    Projectile = 10,
    BreakPoint = 11

}

public class GunSolver : MonoBehaviour
{
    
    public Transform beamSource;
    public RayMarchMaster rayMarchMaster;
    public SerializeStructLaserEffect serializeStructLaserEffect;
    public GameObject collisionParticles;

    [System.NonSerialized] public LaserEffect laserEffect;

    private ParticlesOnLaserCollision _particlesOnLaserCollision;
    private GunBeamInteraction _gunBeamInteraction;

    private void Start()
    {
        _gunBeamInteraction = new GunBeamInteraction();
        _particlesOnLaserCollision = new ParticlesOnLaserCollision(collisionParticles);
        laserEffect = new LaserEffect(serializeStructLaserEffect);
    }
    
    public void UpdateSolver()
    {
        RecalculateGunBeamInteraction(GetRay());
        laserEffect.RecalculateLaser(_gunBeamInteraction);
        
        _particlesOnLaserCollision.UpdateParticles(_gunBeamInteraction);
        CollisionHandling(_gunBeamInteraction);
    }

    private Ray GetRay()
    {
        Ray aimRay = new Ray();
        aimRay.origin = beamSource.position;
        aimRay.direction = beamSource.TransformDirection(Vector3.forward);
        Debug.DrawRay(aimRay.origin, aimRay.direction * 10f, Color.red);
        return aimRay;
    }

    private void RecalculateGunBeamInteraction(Ray ray)
    {
        RaycastHit raycastHit;
        bool isHit = Physics.Raycast(ray, out raycastHit, 50f);
        _gunBeamInteraction.isHit = isHit;
        _gunBeamInteraction.hit = raycastHit;
        _gunBeamInteraction.ray = ray;
    }

    private void CollisionHandling(GunBeamInteraction gunBeamInteraction)
    {
        if (!gunBeamInteraction.isHit) { return; }

        int layerID = gunBeamInteraction.hit.collider.gameObject.layer;
        switch (layerID)
        {
            case (int)LayersID.SDF:
                SDF_Behaviour sDF_Behaviour = gunBeamInteraction.hit.collider.GetComponent<SDF_Behaviour>();
                ReportMeltingAction reportMeltingAction = rayMarchMaster.Melt_SDF_Object(sDF_Behaviour);
                
                switch (reportMeltingAction)
                {
                    case ReportMeltingAction.addedMeltObject:
                        break;
                    case ReportMeltingAction.continueMelting:
                        Ray extendedRay = GetExtendedRay(gunBeamInteraction.ray, gunBeamInteraction.hit.point);
                        
                        RaycastHit raycastHit;
                        if (Physics.Raycast(extendedRay.origin, extendedRay.direction, out raycastHit, 5f, 1 << (int)LayersID.BreakPoint))
                        {
                            BreakPointReceiver breakPointReceiver = raycastHit.collider.gameObject.GetComponent<BreakPointReceiver>();
                            breakPointReceiver.StepBreaking();
                        }
                        Debug.DrawLine(extendedRay.origin, extendedRay.origin + extendedRay.direction, Color.green);
                        break;
                }

                break;
            case (int)LayersID.Projectile:
                ProjectileReceiver projectileReceiver = gunBeamInteraction.hit.collider.GetComponent<ProjectileReceiver>();
                projectileReceiver.SendStepHit();
                break;
        }
    }

    private Ray GetExtendedRay(Ray oldRay, Vector3 oldRayPointHit)
    {
        Ray newRay = new Ray();
        newRay.origin = oldRayPointHit;
        newRay.direction = oldRay.direction;
        return newRay;
    }
}

public struct GunBeamInteraction
{
    public bool isHit;
    public RaycastHit hit;
    public Ray ray;
}

