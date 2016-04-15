using UnityEngine;
using System.Collections;
using System;

public class Metronome : MonoBehaviour
{

    public int BPM = 120; //Beats per minute
    public int beatsInBar;  //top number of time signature
    public int noteValue;   //bottom number of time signature
    private float _beatCounter;   //count the number of beats
    private float _interval;    //the time between beats
    private const float CROTCHET = 4.0f;    //a constant crotchet/quater note value
    public Action beatCallback;

    void Start()
    {
    }

    void Update()
    {
        //check to see if note is an approprate value e.g. whole,2nd,4th,8th,16th,32th notes
        if (!IsPowerOfTwo(noteValue))
            noteValue = 4;

        Time.fixedDeltaTime = CalculateInterval();  //set the fixed update to play tick N.B.* could use co-routine instead. 
    }

    void FixedUpdate()
    {
        _beatCounter+= 0.25f; //incrementbeat;
        quarterBeat();
    }

    private void quarterBeat()
    {
        beatCallback();
    }

    private void beat()
    {
        beatCallback();
    }

    //simple metod to check if our value is a power of 2
    bool IsPowerOfTwo(int value)
    {
        return (value != 0) && ((value & (value - 1)) == 0);
    }

    float CalculateInterval()
    {
        float tickSize = CROTCHET / (float)noteValue;
        float interval = (60.0f / BPM) * tickSize;
        return interval / 4;
    }
}