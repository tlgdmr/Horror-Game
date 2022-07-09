using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLight : MonoBehaviour
{
    Light _streetLight;

    float StreetLightTimeForFlame = 0.05f;
    float StreetLightMinimumIntensity = 0.5f;
    float StreetLightMaximumIntensity = 6;

    private void Start()
    {
        _streetLight = GetComponent<Light>();
        StartCoroutine(StreetLightIntensity());
    }

    IEnumerator StreetLightIntensity()
    {
        while (true)
        {
            float randomIntensityForLight = Random.Range(StreetLightMinimumIntensity, StreetLightMaximumIntensity);
            _streetLight.intensity = randomIntensityForLight;
            yield return new WaitForSeconds(StreetLightTimeForFlame);
        }
    }
}
