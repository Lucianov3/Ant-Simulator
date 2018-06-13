using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayNightCycle : MonoBehaviour
{
    private enum SpeedMode { Normal,Fast,Faster,UberFast,Stop}
    [SerializeField] private TextMeshProUGUI clockText;

    private float timer;
    private Transform light;

    [SerializeField] private SpeedMode Speed;

    [HideInInspector] public int hours;
    [HideInInspector] public int minutes;
    [HideInInspector] public bool hasClockBeenSet;

    private void Start()
    {
        light = transform;
        if (!hasClockBeenSet)
        {
            hours = 12;
            minutes = 0;
            hasClockBeenSet = true;
        }
            UpdateClockText();
            UpdateSun();
    }

    private void Update()
    {
        switch (Input.inputString)
        {
            case "1":
                Speed = SpeedMode.Stop;
                break;
            case "2":
                Speed = SpeedMode.Normal;
                break;
            case "3":
                Speed = SpeedMode.Fast;
                break;
            case "4":
                Speed = SpeedMode.Faster;
                break;
            case "5":
                Speed = SpeedMode.UberFast;
                break;
            default:
                break;
        }

        switch (Speed)
        {
            case SpeedMode.Normal:
                Time.timeScale = 1;
                break;
            case SpeedMode.Fast:
                Time.timeScale = 2;
                break;
            case SpeedMode.Faster:
                Time.timeScale = 10;
                break;
            case SpeedMode.UberFast:
                Time.timeScale = 100;
                break;
            case SpeedMode.Stop:
                Time.timeScale = 0;
                break;
        }
        timer += Time.deltaTime;
        if(timer >= 1)
        {
            timer = 0;
            minutes++;
            if(minutes == 60)
            {
                minutes = 0;
                hours++;
                if(hours == 24)
                {
                    hours = 0;
                }
            }
            UpdateClockText();
            UpdateSun();
        }
    }

    private void UpdateClockText()
    {
        if(hours > 12)
        {
            if(hours >= 22)
            {
                if(minutes >= 10)
                {
                    clockText.text = (hours-12) + ":" + minutes + " PM";
                }
                else
                {
                    clockText.text = (hours - 12) + ":0" + minutes + " PM";
                }
            }
            else
            {
                if (minutes >= 10)
                {
                    clockText.text = "0" + (hours - 12) + ":" + minutes + " PM";
                }
                else
                {
                    clockText.text = "0"+(hours - 12) + ":0" + minutes + " PM";
                }
            }
        }
        else
        {
            if (hours >= 10)
            {
                if (minutes >= 10)
                {
                    clockText.text = hours + ":" + minutes + " AM";
                }
                else
                {
                    clockText.text = hours + ":0" + minutes + " AM";
                }
            }
            else
            {
                if (minutes >= 10)
                {
                    clockText.text = "0" + hours + ":" + minutes + " AM";
                }
                else
                {
                    clockText.text = "0" + hours + ":0" + minutes + " AM";
                }
            }
        }
    }

    private void UpdateSun()
    {
        float sunAngle = (180*hours/24)+((180/24)*minutes/60);
        transform.eulerAngles = new Vector3(sunAngle,-90,-90);
    }
}
