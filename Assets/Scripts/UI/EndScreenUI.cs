using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EndScreenUI : MonoBehaviour
{
    private const string QuitButtonName = "QuitButton";
    private const string PointsLabelName = "PointsLabel";
    private const string TimeLabelName = "TimeLabel";
    private const string MessageLabelName = "MessageLabel";

    private UIDocument _endScreenDocument;

    private Label _messageLabel;
    private Label _pointsLabel;
    private Label _timeLabel;

    private void OnEnable()
    {
        _endScreenDocument = GetComponent<UIDocument>();
        if (_endScreenDocument == null)
        {
            Debug.LogError("No UIDocument found on OverlayManager object! Disabling OverlayManager script.");
            enabled = false;
            return;
        }
        _endScreenDocument.rootVisualElement.Q<Button>(QuitButtonName).clicked += () =>
        {
            Debug.Log("Quit button clicked!");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        };

        _messageLabel = _endScreenDocument.rootVisualElement.Q<Label>(MessageLabelName);
        _pointsLabel = _endScreenDocument.rootVisualElement.Q<Label>(PointsLabelName);
        _timeLabel = _endScreenDocument.rootVisualElement.Q<Label>(TimeLabelName);
    }

    private void Update()
    {
        _messageLabel.text = Game.Finished ? "YOU WON!" : "YOU LOST";
        _pointsLabel.text = $"Points: {Game.Points}";
        _timeLabel.text = $"Time: {Game.TimeFormatted}";
    }
}
