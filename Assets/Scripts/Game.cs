using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Game : MonoBehaviour
{
    private long _gatesPassed;
    private float _timeScale;

    private Coin[] _coins;
    private Gate[] _gates;
    private Player _player;
    private Finish _finish;

    [SerializeField] private SceneUIManager _sceneUIManager;

    public static long Points;
    public static float StartTime;
    public static float? EndTime;
    public static string TimeFormatted => FormatLapTime(TimeSpan.FromSeconds((EndTime ?? Time.time) - StartTime));
    public static bool Finished = false;

    private void Start()
    {
        StartTime = Time.time;
        Time.timeScale = 1f;
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
        _finish.AnimationTriggerPassed += OnFinishAnimationPassed;
    }

    private void OnFinishAnimationPassed(object sender, EventArgs e)
    {
        _player.SpeedUp();
    }

    private void GatePassed(object sender, EventArgs e)
    {
        _gatesPassed++;
    }

    private void OnPlayerCollision(object sender, EventArgs e)
    {
        EndTime = Time.time;

        Invoke("ShowEndScreen", 1f);
    }

    private void CoinCollected(object sender, EventArgs e)
    {
        var coin = (Coin)sender;
        Points += coin.Value;
    }

    private void OnPlayerFinished(object sender, EventArgs e)
    {
        _player.Stop();
        EndTime = Time.time;
        Finished = true;
        Invoke("ShowEndScreen", 1f);
    }

    private void ShowEndScreen()
    {
        _sceneUIManager.ShowEndScreen();
    }

    private static string FormatLapTime(TimeSpan t)
    {
        return String.Format("{0:00}:{1:00}.{2:00}", t.Minutes, t.Seconds, t.Milliseconds);
    }

    public virtual void OnPause(InputValue value)
    {
        if (Time.timeScale == 0)
        {
            _sceneUIManager.UnPause();
        }
        else
        {
            _sceneUIManager.PauseGame();
        }
    }
}