using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ControlsPanel : LobbyPanelBase
{
    [SerializeField] private Button backButton;

    public override void InitPanels(LobbyUIManager UIManager)
    {
        base.InitPanels(UIManager);
        backButton.onClick.AddListener(OnClickGoBack);
    }

    private void OnClickGoBack()
    {
        ClosePanel();
        lobbyUIManager.ShowPanel(LobbyPanelType.MainMenu);
    }
}
