using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("UI Settings")]
    public GameObject levelCompletePanel;
    public Text levelText;
    public float displayDuration = 3f;
    public float completeMessageDuration = 2f;

    private int currentLevel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas != null)
            {
                DontDestroyOnLoad(canvas.gameObject);
            }
            
            // ثبت رویداد تغییر صحنه
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas != null)
            {
                Destroy(canvas.gameObject);
            }
        }
    }

    // این متد هر بار که صحنه عوض می‌شود صدا زده می‌شود
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // آپدیت currentLevel با buildIndex صحنه جدید
        currentLevel = scene.buildIndex;
        Debug.Log("[LevelManager] Scene loaded: " + scene.name + " (buildIndex: " + currentLevel + ")");
        
        // نمایش پیام شروع Level
        StartCoroutine(ShowLevelMessage());
    }

    void Start()
    {
        // فقط برای اولین صحنه
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(ShowLevelMessage());
    }

    IEnumerator ShowLevelMessage()
    {
        yield return new WaitForSeconds(0.1f);
        
        if (levelCompletePanel != null && levelText != null)
        {
            levelText.text = "Level " + (currentLevel + 1);
            levelCompletePanel.SetActive(true);
            
            yield return new WaitForSeconds(displayDuration);
            
            levelCompletePanel.SetActive(false);
        }
    }

    public void CompleteLevel()
    {
        Debug.Log("[LevelManager] Level " + (currentLevel + 1) + " completed!");
        Debug.Log("[LevelManager] Current buildIndex: " + currentLevel);
        
        StartCoroutine(ShowCompleteMessageAndLoadNext());
    }

    IEnumerator ShowCompleteMessageAndLoadNext()
    {
        if (levelCompletePanel != null && levelText != null)
        {
            levelText.text = "Level Complete!";
            levelCompletePanel.SetActive(true);
            
            yield return new WaitForSeconds(completeMessageDuration);
            
            levelCompletePanel.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        
        int nextLevel = currentLevel + 1;
        
        Debug.Log("[LevelManager] Next level to load: " + nextLevel);
        
        if (nextLevel < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("[LevelManager] Loading Level " + (nextLevel + 1));
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            Debug.Log("[LevelManager] Game Complete!");
            if (levelText != null)
            {
                levelText.text = "Game Complete!";
                levelCompletePanel.SetActive(true);
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("[LevelManager] Game Over! Restarting Level " + (currentLevel + 1));
        
        StartCoroutine(ShowGameOverAndRestart());
    }

    IEnumerator ShowGameOverAndRestart()
    {
        if (levelCompletePanel != null && levelText != null)
        {
            levelText.text = "Game Over!";
            levelCompletePanel.SetActive(true);
            
            yield return new WaitForSeconds(2f);
            
            levelCompletePanel.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        
        RestartLevel();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }

    void OnDestroy()
    {
        // حذف رویداد وقتی LevelManager از بین می‌رود
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}