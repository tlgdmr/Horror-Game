using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireController : MonoBehaviour
{
    [SerializeField] Transform cloneCollector;

    CamerRotation mouseY;
    float mouseYvalue;
    float MinBulletRotation = -30f;
    float MaxBulletRotation = 20f;

    ParticleSystem MagicBall;

    public List<ParticleCollisionEvent> collisionEvents;
    public List<ParticleSystem.Particle> enter;
    
    [SerializeField] ParticleSystem ExplosionEffect;

    [SerializeField] GameObject AllEnemyGameObject;

    ParticleSystem.EmissionModule emissionModule;

   

    void OnEnable()
    {
        MagicBall = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        mouseY = FindObjectOfType<CamerRotation>();
        emissionModule = MagicBall.GetComponent<ParticleSystem>().emission;
    }
    void Update()
    {
        FireInputManager();
        SetAim();
    }

    public void SetTriggerModuleList()
    {
        if (MagicBall.trigger.colliderCount > 0)
        {
            for (int i = 0; i < MagicBall.trigger.colliderCount; i++)
            {
                MagicBall.trigger.RemoveCollider(i);
            }
        }

        if (MagicBall.trigger.colliderCount <= 0)
        {
            for (int i = 0; i < AllEnemyGameObject.transform.childCount; i++)
            {
                MagicBall.trigger.SetCollider(i, AllEnemyGameObject.transform.GetChild(i));
            }
        }
    }

    void FireInputManager()
    {
        if (Input.GetButton("Fire1"))
        {
            StartFiring(true);
        }
        else
        {
            StartFiring(false);
        }
    }
    void StartFiring(bool fire)
    {
        emissionModule.enabled = fire;
    }
    void SetAim()
    {
        mouseYvalue -= mouseY.MouseY;
        mouseYvalue = Mathf.Clamp(mouseYvalue, MinBulletRotation, MaxBulletRotation);
        transform.localRotation = Quaternion.Euler(mouseYvalue, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
    }
   
    private void OnParticleCollision(GameObject other)
    {
        MagicBall.GetCollisionEvents(other, collisionEvents);

        ParticleSystem explosion = Instantiate(ExplosionEffect, collisionEvents[0].intersection, Quaternion.identity);

        if (collisionEvents[0].colliderComponent.transform.tag == "Enemy")
        {
            Destroy(explosion.gameObject);
        }

        Destroy(explosion.gameObject, 1);
        explosion.transform.parent = cloneCollector;
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;
        Vector3 aimPosition;
        if (Physics.Raycast(transform.position,transform.forward,out hit))
        {
            aimPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            Gizmos.DrawSphere(aimPosition, 1);
        }
    }
}
