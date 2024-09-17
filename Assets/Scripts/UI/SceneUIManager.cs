using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUIManager : MonoBehaviour
{
    [SerializeField] private InGameOverlayUI overlayUILogic;
    [SerializeField] private InGameMenuUI menuUILogic;

    private InGameOverlayUI _overlayPanel;
    private InGameMenuUI _menuPanel;

    private float _timeScale;

    private void Awake()
    {
        _overlayPanel = Instantiate(overlayUILogic, transform);
        _menuPanel = Instantiate(menuUILogic, transform);
    }

    private void Start()
    {
        _menuPanel.gameObject.SetActive(false);
        _overlayPanel.PauseButtonPressed += OnPauseButtonPressed;
        _menuPanel.ResumeButtonPressed += OnResumeButtonPressed;
    }

    private void OnPauseButtonPressed(object sender, EventArgs e)
    {
        _timeScale = Time.timeScale;
        Time.timeScale = 0;
        _overlayPanel.gameObject.SetActive(false);
        _menuPanel.gameObject.SetActive(true);
    }

    private void OnResumeButtonPressed(object sender, EventArgs e)
    {
        Time.timeScale = _timeScale;
        _overlayPanel.gameObject.SetActive(true);
        _menuPanel.gameObject.SetActive(false);
    }
}

