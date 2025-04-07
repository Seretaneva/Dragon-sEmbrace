using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float fadeDuration = 1f;

    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        ApplyVolumeToMixer(savedVolume);

        Scene currentScene = SceneManager.GetActiveScene();
        HandleMusicForScene(currentScene.name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HandleMusicForScene(scene.name);
    }

    void HandleMusicForScene(string sceneName)
    {
        if (sceneName == "MainMenuScene" || sceneName == "Settings Menu")
        {
            if (!audioSource.isPlaying)
            {
                StartCoroutine(FadeInAndPlay());
            }
        }
        else
        {
            StartCoroutine(FadeOutAndStop());
        }
    }

    void ApplyVolumeToMixer(float volume)
    {
        volume = Mathf.Clamp(volume, 0.001f, 1.0f);
        float dB = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("MusicVolume", dB);
    }

    IEnumerator FadeOutAndStop()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Reset pentru data viitoare
    }

    IEnumerator FadeInAndPlay()
    {
        audioSource.volume = 0f;
        audioSource.Play();

        float targetVolume = 1f;

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }
}
