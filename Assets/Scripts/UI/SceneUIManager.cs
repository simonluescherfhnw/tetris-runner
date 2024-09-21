using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUIManager : MonoBehaviour
{
    [SerializeField] private InGameOverlayUI overlayUILogic;
    [SerializeField] private EndScreenUI endScreenUILogic;
    [SerializeField] private InGameMenuUI menuUILogic;

    private InGameOverlayUI _overlayPanel;
    private EndScreenUI _endScreenPanel;
    private InGameMenuUI _menuPanel;

    private float _timeScale;

    private void Awake()
    {
        _overlayPanel = Instantiate(overlayUILogic, transform);
        _menuPanel = Instantiate(menuUILogic, transform);
        _endScreenPanel = Instantiate(endScreenUILogic, transform);
    }

    private void Start()
    {
        _menuPanel.gameObject.SetActive(false);
        _endScreenPanel.gameObject.SetActive(false);

        _overlayPanel.PauseButtonPressed += OnPauseButtonPressed;
        _menuPanel.ResumeButtonPressed += OnResumeButtonPressed;
    }

    private void OnPauseButtonPressed(object sender, EventArgs e)
    {
        PauseGame();
    }

    public void PauseGame()
    {
        if (Time.timeScale > 0)
        {
            _timeScale = Time.timeScale;
            Time.timeScale = 0;
            _overlayPanel.gameObject.SetActive(false);
            _menuPanel.gameObject.SetActive(true);
            _endScreenPanel.gameObject.SetActive(false);
        }


    }
    public void UnPause()
    {
        Time.timeScale = _timeScale;
        _overlayPanel.gameObject.SetActive(true);
        _menuPanel.gameObject.SetActive(false);
        _endScreenPanel.gameObject.SetActive(false);
    }

    private void OnResumeButtonPressed(object sender, EventArgs e)
    {
        UnPause();
    }

    public void ShowEndScreen()
    {
        _overlayPanel.gameObject.SetActive(false);
        _menuPanel.gameObject.SetActive(false);
        _endScreenPanel.gameObject.SetActive(true);
        Time.timeScale = _timeScale;
    }
}

