using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("UI")]
    public Sprite soundOn;
    public Sprite soundOff;
    public Image buttonImage;

    private AudioSource audioSource;

    void Awake()
    {
        // اگر قبلاً ساخته شده، این یکی حذف بشه
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // AudioSource را از خود آبجکت یا فرزندش پیدا کن
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = GetComponentInChildren<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource پیدا نشد!");
            return;
        }

        // تنظیمات موزیک
        audioSource.loop = true;

        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void ToggleMusic()
    {
        if (audioSource == null)
            return;

        if (audioSource.isPlaying)
        {
            audioSource.Pause();

            if (buttonImage != null && soundOff != null)
                buttonImage.sprite = soundOff;
        }
        else
        {
            audioSource.Play();

            if (buttonImage != null && soundOn != null)
                buttonImage.sprite = soundOn;
        }
    }
}