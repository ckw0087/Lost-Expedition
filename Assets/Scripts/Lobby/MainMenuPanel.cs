using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : LobbyPanelBase
{
    [SerializeField] private Button hostRoomButton;
    [SerializeField] private Button joinRoomButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button quitButton;

    public override void InitPanels(LobbyUIManager UIManager)
    {
        base.InitPanels(UIManager);
        hostRoomButton.onClick.AddListener(OnClickOpenHostPanel);
        joinRoomButton.onClick.AddListener(OnClickOpenJoinPanel);
        controlsButton.onClick.AddListener(OnClickOpenControlsPanel);
        quitButton.onClick.AddListener(OnClickQuit);
    }

    private void OnClickOpenHostPanel()
    {
        ClosePanel();
        lobbyUIManager.ShowPanel(LobbyPanelType.HostRoom);
    }

    private void OnClickOpenJoinPanel()
    {
        ClosePanel();
        lobbyUIManager.ShowPanel(LobbyPanelType.JoinRoom);
    }

    private void OnClickOpenControlsPanel()
    {
        ClosePanel();
        lobbyUIManager.ShowPanel(LobbyPanelType.Controls);
    }

    private void OnClickQuit()
    {
        Application.Quit();
    }
}
