using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public Sprite soundOn;
    public Sprite soundOff;
    public Image buttonImage;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            buttonImage.sprite = soundOff;
        }
        else
        {
            audioSource.Play();
            buttonImage.sprite = soundOn;
        }
    }
}