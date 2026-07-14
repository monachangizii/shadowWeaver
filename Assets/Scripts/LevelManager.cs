using System.Collections;
using System.Collections.Generic;
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
    public float completeMessageDuration = 2f; // مدت نمایش پیام "Level Complete"

    private int currentLevel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        
        // نمایش پیام شروع Level
        StartCoroutine(ShowLevelMessage());
    }

    // نمایش پیام "Level X" در شروع
    IEnumerator ShowLevelMessage()
    {
        if (levelCompletePanel != null && levelText != null)
        {
            levelText.text = "Level " + (currentLevel + 1);
            levelCompletePanel.SetActive(true);
            
            yield return new WaitForSeconds(displayDuration);
            
            levelCompletePanel.SetActive(false);
        }
    }

    // تکمیل Level - نمایش پیام و رفتن به Level بعدی
    public void CompleteLevel()
    {
        Debug.Log("[LevelManager] Level " + (currentLevel + 1) + " completed!");
        
        // نمایش پیام "Level Complete!"
        StartCoroutine(ShowCompleteMessageAndLoadNext());
    }

    // نمایش پیام "Level Complete!" و بارگذاری Level بعدی
    IEnumerator ShowCompleteMessageAndLoadNext()
    {
        if (levelCompletePanel != null && levelText != null)
        {
            levelText.text = "Level Complete!";
            levelCompletePanel.SetActive(true);
            
            Debug.Log("[LevelManager] Showing 'Level Complete!' message");
            
            // صبر به مدت completeMessageDuration
            yield return new WaitForSeconds(completeMessageDuration);
            
            levelCompletePanel.SetActive(false);
            
            // صبر کوتاه برای انتقال
            yield return new WaitForSeconds(0.5f);
            
            int nextLevel = currentLevel + 1;
            
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
}