using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas != null)
            {
                DontDestroyOnLoad(canvas.gameObject);
            }
            
            DontDestroyOnLoad(gameObject);
            
            // ثبت رویداد تغییر صحنه
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas != null && canvas.gameObject != Instance.GetComponentInParent<Canvas>().gameObject)
            {
                Destroy(canvas.gameObject);
            }
            Destroy(gameObject);
            return;
        }
    }

    // این متد هر بار که صحنه عوض می‌شود صدا زده می‌شود
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // آپدیت currentLevel با buildIndex صحنه جدید
        currentLevel = scene.buildIndex;
        Debug.Log("[LevelManager] Scene loaded: " + scene.name + " (buildIndex: " + currentLevel + ")");
        
        // پیدا کردن UI در صحنه جدید
        FindUIElements();
        
        // نمایش پیام شروع Level
        StartCoroutine(ShowLevelMessage());
    }

    void Start()
    {
        // فقط برای اولین صحنه (Level1)
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        FindUIElements();
        StartCoroutine(ShowLevelMessage());
    }

    void FindUIElements()
    {
        if (levelCompletePanel == null)
        {
            levelCompletePanel = GameObject.Find("LevelCompletePanel");
        }
        if (levelText == null)
        {
            Text[] texts = FindObjectsOfType<Text>();
            foreach (Text t in texts)
            {
                if (t.name == "LevelText")
                {
                    levelText = t;
                    break;
                }
            }
        }
    }

    IEnumerator ShowLevelMessage()
    {
        yield return new WaitForSeconds(0.1f); // صبر کوتاه برای اطمینان از لود کامل UI
        
        if (levelCompletePanel != null && levelText != null)
        {
            levelText.text = "Level " + (currentLevel + 1);
            levelCompletePanel.SetActive(true);
            
            yield return new WaitForSeconds(displayDuration);
            
            levelCompletePanel.SetActive(false);
        }
        else
        {
            Debug.LogError("[LevelManager] UI elements are null! Cannot show level message.");
        }
    }

    public void CompleteLevel()
    {
        Debug.Log("[LevelManager] Level " + (currentLevel + 1) + " completed!");
        Debug.Log("[LevelManager] Current buildIndex: " + currentLevel);
        
        FindUIElements();
        StartCoroutine(ShowCompleteMessageAndLoadNext());
    }

    IEnumerator ShowCompleteMessageAndLoadNext()
    {
        if (levelCompletePanel != null && levelText != null)
        {
            levelText.text = "Level Complete!";
            levelCompletePanel.SetActive(true);
            
            Debug.Log("[LevelManager] Showing 'Level Complete!' message");
            
            yield return new WaitForSeconds(completeMessageDuration);
            
            levelCompletePanel.SetActive(false);
            
            yield return new WaitForSeconds(0.5f);
            
            int nextLevel = currentLevel + 1;
            
            Debug.Log("[LevelManager] Next level to load: " + nextLevel);
            
            if (nextLevel < SceneManager.sceneCountInBuildSettings)
            {
                Debug.Log("[LevelManager] Loading Level " + (nextLevel + 1));
                SceneManager.LoadScene(nextLevel);
            }
            else
            {
                Debug.Log("[LevelManager] Game Complete! All levels finished.");
                levelText.text = "Game Complete!";
                levelCompletePanel.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("[LevelManager] UI elements are null! Cannot show complete message.");
            int nextLevel = currentLevel + 1;
            if (nextLevel < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextLevel);
            }
        }
    }

    public void RestartLevel()
    {
        Debug.Log("[LevelManager] Restarting Level " + (currentLevel + 1));
        SceneManager.LoadScene(currentLevel);
    }

    public void GoToLevel(int levelNumber)
    {
        if (levelNumber >= 0 && levelNumber < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(levelNumber);
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}