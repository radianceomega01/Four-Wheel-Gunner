﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{

    private float maxTime;
    private float timeLeft;
    private bool startTimer;

    public static TimerScript Instance;
    public event Action onTimeUp;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    void Start()
    {     
        timeLeft = maxTime;
    }

    
    void Update()
    {
        if (startTimer)
        {

            if (timeLeft > 0)
            {   
                timeLeft -= Time.deltaTime;
            }
            else
            {
                onTimeUp.Invoke();
            }
        }

    }
    public void SetTime(float time)
    {
        timeLeft = time;
        maxTime = time;
    }

    public void SetStatusOfTime(bool Status)
    {
        startTimer = Status;
    }
}

