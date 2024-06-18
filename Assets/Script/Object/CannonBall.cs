using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : Bullet
{
    public GameObject particle;
    private ParticleSystem boom;

    private void Start()
    {
        boom = particle.GetComponent<ParticleSystem>();
    }

    new void Update()
    {
        base.Update();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            boom.Play();
            AudioManager.instance.boom.Play();
        }
        else return;
    }
}
