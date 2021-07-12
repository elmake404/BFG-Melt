using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEffect
{
    public float MaxLength;

    private LineRenderer Laser;

    public LaserEffect(SerializeStructLaserEffect serialize)
    {
        MaxLength = serialize.MaxLength;
        Laser = serialize.Laser;
    }

    public void RecalculateLaser(GunBeamInteraction gunBeamInteraction)
    {
        Ray ray = gunBeamInteraction.ray;
        RaycastHit hit = gunBeamInteraction.hit;
        Laser.SetPosition(0, ray.origin);
        
        if (gunBeamInteraction.isHit)
        {
            Laser.SetPosition(1, hit.point);
        }
        else
        {
            var EndPos = ray.origin + ray.direction * MaxLength;
            Laser.SetPosition(1, EndPos);
            
        }
    }

}


