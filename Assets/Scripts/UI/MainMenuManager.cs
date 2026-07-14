using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;      
    public GameObject levelSelectPanel;   

    [Header("Scene Loading")]
    public string gameSceneName = "MainScene"; 

    private void Start()
    {
        ShowMainMenu();
    }


    public void OnPlayButton()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnNewGameButton()
    {

        SceneManager.LoadScene(gameSceneName);
    }

    public void OnLevelSelectButton()
    {
        ShowLevelSelect();
    }

    public void OnQuitButton()
    {
        Debug.Log("Quitting game...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    public void OnBackButton()
    {
        ShowMainMenu();
    }

    public void OnLevelSelected(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }


    private void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
    }

    private void ShowLevelSelect()
    {
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }
}
