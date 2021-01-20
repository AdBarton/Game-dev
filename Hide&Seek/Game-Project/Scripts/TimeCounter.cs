using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour 
{
    public float TimeStart;
    public Text time;
    
    private int min=0;
    private int sec = 0;
    private bool stime=true;
    // Start is called before the first frame update
    void Start()
    {
        time.text = TimeStart.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        if (stime) {
            TimeStart += Time.deltaTime;
            if (TimeStart >= 60) {
                min++;
                TimeStart = 0;
            }
            sec = (int)TimeStart;
            time.text =min.ToString("D2")+":"+sec.ToString("D2");
        }
    }

    public float StopTime() {
        stime = false;
        return TimeStart;
    }
    public void StartTime() {
        stime = true;
    }

    public int getMin()
    {
        return min;
    }
    public int getSec() {
        return sec;
    }
}
