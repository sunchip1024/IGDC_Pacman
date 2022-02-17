using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float time = 0;
    private float goal = 0;

    private bool isStart = false;
    
    // Update is called once per frame
    void Update()
    {
        if (time >= goal) return;
        if (!isStart) return;
        time += Time.deltaTime;    
    }

    public void startTimer(float goal) 
    { 
        this.goal = goal; 
        this.time = 0; 
        isStart = true; 
    }

    public void stopTimer() { isStart = false; }
    public bool isTimerEnd() { return time >= goal; }

    public float getNowTime() { return time; }
}
