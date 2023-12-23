using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UsernamePanel : LobbyPanelBase
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private Button createUsernameButton;

    private const int Username_Character = 2;

    public override void InitPanels(LobbyUIManager UIManager)
    {
        base.InitPanels(UIManager);
        createUsernameButton.interactable = false;
        createUsernameButton.onClick.AddListener(OnClickCreateUsername);
        usernameInputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    private void OnInputValueChanged(string value)
    {
        createUsernameButton.interactable = value.Length >= Username_Character;
    }

    private void OnClickCreateUsername()
    {
        string username = usernameInputField.text;

        if (username.Length >= Username_Character)
        {
            ClosePanel();
            lobbyUIManager.ShowPanel(LobbyPanelType.MainMenu);
        }
    }
}
