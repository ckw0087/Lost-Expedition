using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fusion;

public class JoinRoomPanel : LobbyPanelBase
{
    [SerializeField] private TMP_InputField roomInputField;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button joinRandomButton;
    [SerializeField] private Button backButton;

    private NetworkRunnerController networkRunnerController;
    private const int Room_Character = 2;

    public override void InitPanels(LobbyUIManager UIManager)
    {
        base.InitPanels(UIManager);

        networkRunnerController = GlobalManager.Instance.NetworkRunnerController;

        joinButton.interactable = false;
        joinButton.onClick.AddListener(OnClickJoinRoom);
        joinRandomButton.onClick.AddListener(OnClickJoinRandomRoom);
        backButton.onClick.AddListener(OnClickGoBack);
        roomInputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    private void OnInputValueChanged(string value)
    {
        joinButton.interactable = value.Length >= Room_Character;
    }

    private void OnClickJoinRoom()
    {
        if (roomInputField.text.Length >= Room_Character)
        {
            //ClosePanel();
            Debug.Log($"--Joining Room: {roomInputField.text} --");
            networkRunnerController.StartGame(GameMode.Client, roomInputField.text);
        }
    }

    private void OnClickJoinRandomRoom()
    {
        //ClosePanel();
        Debug.Log("--Joining Random Room--");
        networkRunnerController.StartGame(GameMode.AutoHostOrClient, string.Empty);
    }

    private void OnClickGoBack()
    {
        ClosePanel();
        lobbyUIManager.ShowPanel(LobbyPanelType.MainMenu);
    }
}
