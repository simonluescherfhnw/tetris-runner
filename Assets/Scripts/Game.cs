using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Game : MonoBehaviour
{
    private float _startTime;
    private long _points;
    private float _timeScale;

    private void Start()
    {
        _startTime = Time.time;
        useGUILayout = true;
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        GUILayout.Label("TIME: " + (Time.time - _startTime));
        GUILayout.Label("POINTS: " + _points);
        GUILayout.EndArea();
    }

    private string FormatLapTime(TimeSpan t)
    {
        return String.Format("{0:00}:{1:00}.{2:00}", t.Minutes, t.Seconds, t.Milliseconds);
    }

    public virtual void OnPause(InputValue value)
    {

        if (Time.timeScale == 0)
        {
            Time.timeScale = _timeScale;
        }
        else
        {
            _timeScale = Time.timeScale;
            Time.timeScale = 0;
        }

    }
}