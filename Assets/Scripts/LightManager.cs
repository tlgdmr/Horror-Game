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


    [SerializeField] Light DirectionalLight;

    TimeManager TimeManager;

    StreetLight _streetLight;
    CandleLight _candleLight;

    private void Start()
    {
        TimeManager = FindObjectOfType<TimeManager>();
        StartCoroutine(TorchFlameIntensity());
        _streetLight = FindObjectOfType<StreetLight>();
        _candleLight = FindObjectOfType<CandleLight>();
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
            _streetLight.enabled = true;
            _candleLight.enabled = true;
            FlameIntensity.enabled = true;
        }
        else
        {
            _streetLight.enabled = false;
            _candleLight.enabled = false;
            FlameIntensity.enabled = false;
        }
    }
}
