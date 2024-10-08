using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameMenuUI : MonoBehaviour
{
    private const string QuitButtonName = "QuitButton";
    private const string ResumeButtonName = "ResumeButton";

    public event EventHandler ResumeButtonPressed;
    public event EventHandler QuitButtonPressed;
    
    protected virtual void OnResume()
    {
        ResumeButtonPressed?.Invoke(this, EventArgs.Empty);
    }
    protected virtual void OnQuit()
    {
        QuitButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    private UIDocument _menuDocument;

    private void OnEnable()
    {
        _menuDocument = GetComponent<UIDocument>();
        if (_menuDocument == null)
        {
            Debug.LogError("No UIDocument found on OverlayManager object! Disabling OverlayManager script.");
            enabled = false;
            return;
        }
        _menuDocument.rootVisualElement.Q<Button>(QuitButtonName).clicked += () =>
        {
            Debug.Log("Quit button clicked!");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        };
        _menuDocument.rootVisualElement.Q<Button>(ResumeButtonName).clicked += () =>
        {
            Debug.Log("Resume button clicked!");
            OnResume();
        };
    }
}
