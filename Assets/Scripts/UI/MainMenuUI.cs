using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    private const string StartButtonName = "StartButton";
    private const string LevelDropDownName = "LevelDropDown";

    private UIDocument _mainMenuDocument;

    private void OnEnable()
    {
        _mainMenuDocument = GetComponent<UIDocument>();
        if (_mainMenuDocument == null)
        {
            Debug.LogError("No UIDocument found on OverlayManager object! Disabling OverlayManager script.");
            enabled = false;
            return;
        }
        _mainMenuDocument.rootVisualElement.Q<Button>(StartButtonName).clicked += () =>
        {
            Debug.Log("Start Button Pressed");
            int sceneNr = _mainMenuDocument.rootVisualElement.Q<DropdownField>(LevelDropDownName).index + 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNr);
        };
    }
}
