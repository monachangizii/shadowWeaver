using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// این اسکریپت رو روی یه GameObject خالی تو صحنه MainMenu بذار (مثلا اسمش رو بذار "MenuManager")
/// و بعد رفرنس‌های زیر رو تو Inspector پر کن.
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;      // پنلی که شامل دکمه‌های Play / Level Select / Quit هست
    public GameObject levelSelectPanel;   // همون LevelSelectPanel که تو صحنه داری

    [Header("Scene Loading")]
    public string gameSceneName = "MainScene"; // اسم صحنه‌ی اصلی بازی که با Play باز میشه

    private void Start()
    {
        ShowMainMenu();
    }

    // ---------- دکمه‌های منوی اصلی ----------

    public void OnPlayButton()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnNewGameButton()
    {
        // اگه سیستم Save/Load داری (مثلا کلاس SaveSystem)، اینجا باید
        // متد پاک کردن یا ریست کردن سیو رو صدا بزنی، مثلا:
        // SaveSystem.DeleteSave();
        // یا: SaveSystem.ResetProgress();

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

    // ---------- دکمه‌های داخل پنل Level Select ----------

    public void OnBackButton()
    {
        ShowMainMenu();
    }

    // این متد رو می‌تونی به هر دکمه‌ی مرحله وصل کنی و شماره مرحله رو بدی
    public void OnLevelSelected(int levelIndex)
    {
        // مثلا هر مرحله یه صحنه جدا باشه: "Level1", "Level2", ...
        SceneManager.LoadScene("Level" + levelIndex);
    }

    // ---------- توابع کمکی برای سوییچ بین پنل‌ها ----------

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
