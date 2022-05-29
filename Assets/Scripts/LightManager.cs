using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] Light FlameIntensity;

    float WaitingTimeForFlame = 0.01f;
    float FlameMinimumIntensity = 8;
    float FlameMaximumIntensity = 10;


    [SerializeField] Light[] StreetLight;

    float StreetLightTimeForFlame = 0.05f;
    float StreetLightMinimumIntensity = 0;
    float StreetLightMaximumIntensity = 6;

    [SerializeField] Light DirectionalLight;

    TimeManager TimeManager;

    private void Start()
    {
        TimeManager = FindObjectOfType<TimeManager>();
        StartCoroutine(TorchFlameIntensity());
        StartCoroutine(StreetLightIntensity());
    }
    private void Update()
    {
        RotatingLight();
        LightController();
    }
    IEnumerator TorchFlameIntensity()
    {
        while (true)
        {
            float randomIntensityForFlame = UnityEngine.Random.Range(FlameMinimumIntensity, FlameMaximumIntensity);
            FlameIntensity.intensity = randomIntensityForFlame;
            yield return new WaitForSeconds(WaitingTimeForFlame);
        }
    }
    IEnumerator StreetLightIntensity()
    {
        while (true)
        {
            float randomIntensityForLight = UnityEngine.Random.Range(StreetLightMinimumIntensity, StreetLightMaximumIntensity);
            foreach (var light in StreetLight)
            {
                light.intensity = randomIntensityForLight;
            }
            yield return new WaitForSeconds(StreetLightTimeForFlame);
        }
    }
    void RotatingLight()
    {
        if (TimeManager != null)
        {
            DirectionalLight.transform.localRotation = Quaternion.Euler(TimeManager.RotationValue(), 0, 0);
        }
    }
    void LightController()
    {
        if (TimeManager.Hour >= 18 || TimeManager.Hour <= 6)
        {
            foreach (var light in StreetLight)
            {
                light.enabled = true;
            }
            FlameIntensity.enabled = true;
        }
        else
        {
            foreach (var light in StreetLight)
            {
                light.enabled = false;
            }
            FlameIntensity.enabled = false;
        }
    }
}
