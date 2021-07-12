using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnLaserCollision
{
    private List<ParticlesDataStruct> _particles;
    private ParticleSystem.Burst zeroBurst;

    public ParticlesOnLaserCollision(GameObject particles)
    {
        zeroBurst.count = 0;

        _particles = new List<ParticlesDataStruct>();
        GameObject instance = GameObject.Instantiate(particles);
        _particles.Add(new ParticlesDataStruct(instance));
        _particles.Add(new ParticlesDataStruct(instance.transform.GetChild(0).gameObject));

    }

    public void UpdateParticles(GunBeamInteraction gunBeamInteraction)
    {
        if (gunBeamInteraction.isHit)
        {
            //Debug.Log(true);
            for (int i = 0; i < _particles.Count; i++)
            {
                if (_particles[i].isRootParticlesGameobject)
                {
                    _particles[i].gameObjectParticles.transform.position = gunBeamInteraction.hit.point;
                }
                _particles[i].particleSystem.emission.SetBurst(0, _particles[i].originalBurst);
            }
        }
        else
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                _particles[i].particleSystem.emission.SetBurst(0, zeroBurst);
            }
        }
    }
    
}

public struct ParticlesDataStruct
{
    public GameObject gameObjectParticles;
    public ParticleSystem particleSystem;
    public bool isRootParticlesGameobject;
    public ParticleSystem.Burst originalBurst;

    public ParticlesDataStruct(GameObject particles)
    {
        gameObjectParticles = particles;
        particleSystem = particles.GetComponent<ParticleSystem>();
        isRootParticlesGameobject = particles.transform.childCount != 0 ? true : false;

        ParticleSystem.Burst gettedBurst = particleSystem.emission.GetBurst(0);
        originalBurst = new ParticleSystem.Burst();
        originalBurst.count = gettedBurst.count;
        originalBurst.cycleCount = gettedBurst.cycleCount;
        originalBurst.maxCount = gettedBurst.maxCount;
        originalBurst.minCount = gettedBurst.minCount;
        originalBurst.probability = gettedBurst.probability;
        originalBurst.repeatInterval = gettedBurst.repeatInterval;
        originalBurst.time = gettedBurst.time;
    }
}