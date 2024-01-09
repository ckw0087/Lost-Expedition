using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDiamond : Collectible
{
    [SerializeField] private ParticleSystem collectedParticle;

    protected override void Collect()
    {
        PlayParticle();
    }

    private void PlayParticle()
    {
        if (collectedParticle != null)
        {
            collectedParticle.Play();
            Debug.Log("Particle");
        }       
    }
}
