using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPanelBase : MonoBehaviour
{
    public enum LobbyPanelType
    {
        None,
        Username,
        MainMenu,
        HostRoom,
        JoinRoom,
        Controls
    }

    [field:SerializeField] public LobbyPanelType PanelType { get; private set; }

    protected LobbyUIManager lobbyUIManager;

    public virtual void InitPanels(LobbyUIManager UIManager)
    {
        lobbyUIManager = UIManager;
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    protected void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
