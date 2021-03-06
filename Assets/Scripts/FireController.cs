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

    [SerializeField] ParticleSystem ExplosionEffect;

    private void Start()
    {
        MagicBall = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        mouseY = FindObjectOfType<CamerRotation>();
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
        explosion.transform.parent = cloneCollector;
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
        var emissionModule = MagicBall.GetComponent<ParticleSystem>().emission;
        emissionModule.enabled = fire;
    }
    void SetAim()
    {
        mouseYvalue -= mouseY.MouseY;
        mouseYvalue = Mathf.Clamp(mouseYvalue, MinBulletRotation, MaxBulletRotation);
        transform.localRotation = Quaternion.Euler(mouseYvalue, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
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
