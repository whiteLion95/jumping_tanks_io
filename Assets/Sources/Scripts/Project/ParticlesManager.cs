using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : ParticlesManagerCore
{
    private void OnEnable()
    {
        Projectile.OnExplode += OnProjectileExplodeHandler;
        Tank.OnCannonShoot += OnCannonShootHandler;
        Character.OnDie += OnCharacterDiedHandler;
    }

    private void OnDisable()
    {
        Projectile.OnExplode -= OnProjectileExplodeHandler;
        Tank.OnCannonShoot -= OnCannonShootHandler;
        Character.OnDie -= OnCharacterDiedHandler;
    }

    private void OnProjectileExplodeHandler(ProjectileCollider projCollider)
    {
        Particles particleName;

        if (projCollider.gameObject.CompareTag("FireBall"))
            particleName = Particles.FireballExplosion;
        else
            particleName = Particles.BulletExplosion;

        PlayParticle(particleName, projCollider.transform.position, projCollider.transform.rotation);
    }

    private void OnCannonShootHandler(Cannon shootedCannon)
    {
        Particles particleName;

        if (shootedCannon.GetComponentInParent<Character>() is Player && FireballPerk.IsActive)
            particleName = Particles.FireballShoot;
        else
            particleName = Particles.Shoot;

        PlayParticle(particleName, shootedCannon.transform.position, shootedCannon.transform.rotation);
    }

    private void OnCharacterDiedHandler(Character diedCharacter)
    {
        PlayParticle(Particles.CharacterDeath, diedCharacter.DeathParticlePlace.position, Quaternion.identity);
    }
}
