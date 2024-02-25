﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ParticlesData", menuName = "ScriptableObjects/ParticlesData")]

public class ParticlesData : ScriptableObject
{
    [SerializeField] private List<ParticleData> particlesData;

    public ParticleData this[Particles particleName]
    {
        get
        {
            foreach (ParticleData particleData in particlesData)
            {
                if (particleData.ParticleName == particleName)
                {
                    return particleData;
                }
            }

            return null;
        }
    }
}

[Serializable]
public class ParticleData
{
    [SerializeField] private Particles particleName;
    [SerializeField] private GameObject particleObj;
    [SerializeField] private float scale = 1f;

    public Particles ParticleName { get { return particleName; } }
    public GameObject Prefab { get { return particleObj; } }
    public Vector3 Scale { get { return new Vector3(scale, scale, scale); } }
}

public enum Particles
{
    //Write here names of all particles that will be in your project
    BulletExplosion,
    Shoot,
    FireballExplosion,
    FireballShoot,
    CharacterDeath
}
