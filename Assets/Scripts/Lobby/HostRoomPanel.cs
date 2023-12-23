using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fusion;
using System;

public class HostRoomPanel : LobbyPanelBase
{
    [SerializeField] private TMP_InputField roomInputField;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button backButton;

    private NetworkRunnerController networkRunnerController;
    private const int Room_Character = 2;

    public override void InitPanels(LobbyUIManager UIManager)
    {
        base.InitPanels(UIManager);

        networkRunnerController = GlobalManager.Instance.NetworkRunnerController;

        hostButton.interactable = false;
        hostButton.onClick.AddListener(OnClickHostRoom);
        backButton.onClick.AddListener(OnClickGoBack);
        roomInputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    private void OnInputValueChanged(string value)
    {
        hostButton.interactable = value.Length >= Room_Character;
    }

    private void OnClickHostRoom()
    {
        if (roomInputField.text.Length >= Room_Character)
        {
            //ClosePanel();
            Debug.Log($"--Hosting Room: {roomInputField.text} --");
            networkRunnerController.StartGame(GameMode.Host, roomInputField.text);
        }
    }

    private void OnClickGoBack()
    {
        ClosePanel();
        lobbyUIManager.ShowPanel(LobbyPanelType.MainMenu);
    }
}
