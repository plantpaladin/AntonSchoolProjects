using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public delegate void finisher();

public class Timer : MonoBehaviour 
{
    private float timeLeft;
    private bool countdown;
    private Text timerText;
    private finisher onCountDownFinish;
	// Use this for initialization
	void Awake () 
    {
        timeLeft = 0.0f;
        countdown = false;
        timerText = GetComponent<Text>();
	}

    public void startCountdown(float timeCountdown,finisher countFinish)
    {
        onCountDownFinish = countFinish;
        timeLeft = timeCountdown;
        countdown = true;
    }

    public void resetCountDown(float timeCountdown)
    {
        timeLeft = timeCountdown;
        countdown = true;
    }
    public void timePenalty(float penalty)
    {
        timeLeft -= penalty;
        checkTimeLeft();
    }
	// Update is called once per frame
	void Update () 
    {
        if (countdown == true)
        {
            timeLeft -= Time.deltaTime;
            checkTimeLeft();            
        } 

	}
    private void checkTimeLeft()
    {
        timerText.text = "Time left = " + (int)timeLeft;
         if (timeLeft <= 0f)
            {
                countdown = false;
                timerText.text = "Finished";
                onCountDownFinish();
                
            }

         
    }
}
