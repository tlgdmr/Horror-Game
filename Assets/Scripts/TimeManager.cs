using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    int hour;
    public int Hour { get { return hour; } }

    int minute;
  
    int mainTime = 12;

    int defaultRotation = 90;
  
    TextMeshProUGUI TextMeshProTime;

    void Awake()
    {
        hour = DateTime.Now.Hour;
        minute = DateTime.Now.Minute;
        TextMeshProTime = (TextMeshProUGUI)FindObjectOfType(typeof(TextMeshProUGUI));
        StartCoroutine(SetTime());
    }
    IEnumerator SetTime()
    {
        while (true)
        {
            TextMeshProTime.text = hour + ":" + minute.ToString("00");
            yield return new WaitForSeconds(0.0005f);
            minute += 1;
            if (minute == 60)
            {
                hour += 1;
                minute = 00;
            }
            else if (hour == 24)
            {
                hour = 0;
            }
        }
    }
    public float RotationValue()
    {
        float currentHour = hour;
        float currentMinute = minute;

        float difference = ((currentHour - mainTime) * 60) + currentMinute;

        float LightRotationValue = defaultRotation + (difference / 4);

        return LightRotationValue;
    }


}
