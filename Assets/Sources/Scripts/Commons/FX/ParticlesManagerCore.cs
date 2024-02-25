using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

/// <summary>
/// A base class for particles managers
/// </summary>
public class ParticlesManagerCore : MonoBehaviour
{
    [SerializeField] protected ParticlesData particlesData;

    protected void PlayParticle(Particles particleName, Vector3 position, Quaternion rotation)
    {
        ParticleSystem playingParticle = EZ_PoolManager.Spawn(particlesData[particleName].Prefab.transform, position, rotation).GetComponentInChildren<ParticleSystem>();

        playingParticle.transform.localScale = particlesData[particleName].Scale;
        playingParticle.Play();
        StartCoroutine(DespawnParticle(playingParticle));
    }

    protected IEnumerator DespawnParticle(ParticleSystem particle)
    {
        yield return new WaitForSeconds(particle.main.duration);
        EZ_PoolManager.Despawn(particle.transform.parent);
    }
}