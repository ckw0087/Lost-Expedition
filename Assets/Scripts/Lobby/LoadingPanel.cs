using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button cancelButton;

    private NetworkRunnerController networkRunnerController;

    private void Start()
    {
        networkRunnerController = GlobalManager.Instance.NetworkRunnerController;
        networkRunnerController.OnStartedRunnerConnection += OnStartedRunnerConnection;
        networkRunnerController.OnPlayerJoinedSuccessfully += OnPlayerJoinedSuccessfully;

        cancelButton.onClick.AddListener(networkRunnerController.ShutdownRunner);

        gameObject.SetActive(false);
    }

    private void OnStartedRunnerConnection()
    {
        gameObject.SetActive(true);
    }

    private void OnPlayerJoinedSuccessfully()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        networkRunnerController.OnStartedRunnerConnection -= OnStartedRunnerConnection;
        networkRunnerController.OnPlayerJoinedSuccessfully -= OnPlayerJoinedSuccessfully;
    }
}
