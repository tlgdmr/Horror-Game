using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingIntensity : MonoBehaviour
{
    Light lightingIntensity;
    [SerializeField] float WaitingTime = 0.01f;
    [SerializeField] float MinimumIntensity = 3;
    [SerializeField] float MaximumIntensity = 10;

    private void Start()
    {
        lightingIntensity = GetComponent<Light>();
        StartCoroutine(ChangeIntensity());
    }
   IEnumerator ChangeIntensity()
    {
        while (true)
        {
            float randomNumber = Random.Range(MinimumIntensity, MaximumIntensity);
            lightingIntensity.intensity = randomNumber;
            yield return new WaitForSeconds(WaitingTime);

        }
    }
}
