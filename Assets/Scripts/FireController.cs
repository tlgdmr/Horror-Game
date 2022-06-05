using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireController : MonoBehaviour
{
    [SerializeField] Transform parent;

    ParticleSystem MagicBall;

    public List<ParticleCollisionEvent> collisionEvents;

    [SerializeField] ParticleSystem ExplosionEffect;

    RaycastHit hit;

    Vector3 hitPosition;

    TorchAnimationController TorchAnimation;

    private void Start()
    {
        MagicBall = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        TorchAnimation = FindObjectOfType<TorchAnimationController>();
    }
    void Update()
    {
        FireInputManager();
        SetAim();
    }

    private void OnParticleCollision(GameObject other)
    {
        MagicBall.GetCollisionEvents(other, collisionEvents);

        ParticleSystem explosion = Instantiate(ExplosionEffect, collisionEvents[0].intersection, Quaternion.identity);
        Destroy(explosion.gameObject, 1);
        explosion.transform.parent = parent;
    }
    void FireInputManager()
    {
        if (Input.GetButton("Fire1"))
        {
            if (TorchAnimation._TorchAnimation.isActiveAndEnabled)
            {
                TorchAnimation._TorchAnimation.enabled = false;
            }
            StartFiring(true);
        }
        else
        {
            StartFiring(false);
        }
    }
    void StartFiring(bool fire)
    {
        var emissionModule = MagicBall.GetComponent<ParticleSystem>().emission;
        emissionModule.enabled = fire;
    }
    void SetAim()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            hitPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(hitPosition, 1);
    }
}
