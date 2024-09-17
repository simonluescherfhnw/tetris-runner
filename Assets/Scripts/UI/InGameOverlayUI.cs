using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameOverlayUI : MonoBehaviour
{
    private const string PauseButtonName = "PauseButton";

    public event EventHandler PauseButtonPressed;

    protected virtual void OnPause()
    {
        PauseButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    private UIDocument _overlayDocument;

    private void OnEnable()
    {
        _overlayDocument = GetComponent<UIDocument>();
        if (_overlayDocument == null)
        {
            Debug.LogError("No UIDocument found on OverlayManager object! Disabling OverlayManager script.");
            enabled = false;
            return;
        }
        _overlayDocument.rootVisualElement.Q<Button>(PauseButtonName).clicked += () =>
        {
            Debug.Log("Resume button clicked!");
            OnPause();
        };
    }
}
