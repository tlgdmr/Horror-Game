using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingIntensity : MonoBehaviour
{
    Light lightingIntensity;


    private void Start()
    {
        lightingIntensity = GetComponent<Light>();
        ChangeIntensity();
    }
    
    void ChangeIntensity()
    {
        while (true)
        {
            float randomNumber = Random.Range(3, 8);
            lightingIntensity.intensity = randomNumber;
        }
    }
}
