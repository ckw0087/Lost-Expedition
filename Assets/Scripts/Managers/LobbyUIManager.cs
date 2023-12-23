using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] private LobbyPanelBase[] lobbyPanels;
    [SerializeField] private LoadingPanel loadingPanel;

    // Start is called before the first frame update
    private void Start()
    {
        foreach (var panel in lobbyPanels)
        {
            panel.InitPanels(this);
        }

        Instantiate(loadingPanel);
    }

    public void ShowPanel(LobbyPanelBase.LobbyPanelType type)
    {
        foreach (var panel in lobbyPanels)
        {
            if (panel.PanelType == type)
            {
                panel.ShowPanel();
                break;
            }
        }
    }
}
