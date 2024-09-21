using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private MainMenuUI mainMenuPanelPrefab;
    private MainMenuUI _mainMenuPanel;

    private void Awake()
    {
        _mainMenuPanel = Instantiate(mainMenuPanelPrefab, transform);
    }
}