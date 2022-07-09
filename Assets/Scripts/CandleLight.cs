using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleLight : MonoBehaviour
{
    Light Candle;

    float CandleTimeForFlame = 0.1f;
    float CandleMinimumIntensity = 1;
    float CandleMaximumIntensity = 4;

    private void Start()
    {
        Candle = GetComponent<Light>();
        StartCoroutine(CandleIntensity());
    }

    
    IEnumerator CandleIntensity()
    {
        while (true)
        {
            float randomIntensityForLight = Random.Range(CandleMinimumIntensity, CandleMaximumIntensity);
            Candle.intensity = randomIntensityForLight;
            yield return new WaitForSeconds(CandleTimeForFlame);
        }
    }
}
