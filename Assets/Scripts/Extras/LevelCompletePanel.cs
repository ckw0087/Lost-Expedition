using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCompletePanel : MonoBehaviour
{
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button restartLevelButton;
    [SerializeField] private Button mainMenuButton;

    private int currentSceneIndex;

    // Start is called before the first frame update
    private void Start()
    {
        gameObject.SetActive(false);

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        nextLevelButton.onClick.AddListener(OnClickNextLevel);
        restartLevelButton.onClick.AddListener(OnClickRestartLevel);
        mainMenuButton.onClick.AddListener(OnClickMainMenu);
    }

    private void OnClickNextLevel()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private void OnClickRestartLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void OnClickMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
