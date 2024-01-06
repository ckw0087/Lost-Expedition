using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private GameObject mainObject;
    [SerializeField] private Button mainPlayButton;
    [SerializeField] private Button mainControlsButton;
    [SerializeField] private Button mainQuitButton;

    [Header("Controls")]
    [SerializeField] private GameObject controlsObject;
    [SerializeField] private Button controlsPlayButton;
    [SerializeField] private Button controlsBackButton;

    [Header("Levels")]
    [SerializeField] private GameObject levelsObject;
    [SerializeField] private Button levelsBackButton;

    // Start is called before the first frame update
    private void Start()
    {
        mainObject.SetActive(true);
        controlsObject.SetActive(false);
        levelsObject.SetActive(false);

        mainPlayButton.onClick.AddListener(OnPlayButtonClicked);
        mainControlsButton.onClick.AddListener(OnControlsButtonClicked);
        mainQuitButton.onClick.AddListener(OnQuitButtonClicked);

        controlsPlayButton.onClick.AddListener(OnPlayButtonClicked);
        controlsBackButton.onClick.AddListener(OnBackButtonClicked);

        levelsBackButton.onClick.AddListener(OnBackButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        mainObject.SetActive(false);
        controlsObject.SetActive(false);
        levelsObject.SetActive(true);
    }

    private void OnControlsButtonClicked()
    {
        mainObject.SetActive(false);
        controlsObject.SetActive(true);
        levelsObject.SetActive(false);
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    private void OnBackButtonClicked()
    {
        mainObject.SetActive(true);
        controlsObject.SetActive(false);
        levelsObject.SetActive(false);
    }
}
