using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Game : MonoBehaviour
{
    private float _startTime;
    private float? _endTime;
    private long _points;
    private long _gatesPassed;
    private float _timeScale;

    private Coin[] _coins;
    private Gate[] _gates;
    private Player _player;
    private Finish _finish;

    private void Start()
    {
        _startTime = Time.time;
        useGUILayout = true;
        _coins = FindObjectsOfType<Coin>();
        _gates = FindObjectsOfType<Gate>();
        _player = FindObjectOfType<Player>();
        _finish = FindObjectOfType<Finish>();

        foreach (var coin in _coins)
        {
            coin.Collected += CoinCollected;
        }

        foreach (var gate in _gates)
        {
            gate.GatePassed += GatePassed;
        }

        _player.Collided += OnPlayerCollision;
        _finish.Passed += OnPlayerFinished;
    }

    private void GatePassed(object sender, EventArgs e)
    {
        _gatesPassed++;
    }

    private void OnPlayerCollision(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void CoinCollected(object sender, EventArgs e)
    {
        var coin = (Coin)sender;
        _points += coin.Value;
    }

    private void OnPlayerFinished(object sender, EventArgs e)
    {
        _player.Stop();
        _endTime = Time.time;
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        GUILayout.Label("TIME: " + FormatLapTime(TimeSpan.FromSeconds((_endTime ?? Time.time) - _startTime)));
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