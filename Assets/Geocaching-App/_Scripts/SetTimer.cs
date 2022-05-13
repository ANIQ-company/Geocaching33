using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTimer : MonoBehaviour
{
    [SerializeField] private Text timerDisplayText;
    [SerializeField] private Slider timerSlider;

    [SerializeField] private Text
        timerAdminCounterText,
        timerUserCounterText;

    [SerializeField] private string test;

    private float setTimer;


    private void Start()
    {
        timerDisplayText.text = "";
        timerSlider.maxValue *= 60;
    }

    private void Update()
    {
        TimerCounter();
        DisplayTime(timerSlider.value);
    }

    public void SetTimerOnSlider()
    {
        timerDisplayText.text = timerSlider.value.ToString("F");
        float minutes = Mathf.FloorToInt(timerSlider.value / 60);
        float seconds = Mathf.FloorToInt(timerSlider.value % 60);

        timerDisplayText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetTimerOnButton()
    {
    }

    private void TimerCounter()
    {
        if (timerSlider.value > 0)
        {
            timerSlider.value -= Time.deltaTime;
        }
        else
        {
            timerSlider.value = 0;
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        if (timeToDisplay == 0)
        {
            timerAdminCounterText.text = "Set Timer";
        }
        else
        {
            timerAdminCounterText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerUserCounterText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            Debug.Log(timeToDisplay);
        }
    }
}