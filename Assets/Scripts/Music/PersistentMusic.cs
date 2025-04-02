using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;

    [SerializeField] private AudioMixer audioMixer;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        var audio = GetComponent<AudioSource>();
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);

        // Aplică doar în mixer!
        ApplyVolumeToMixer(savedVolume);

        if (!audio.isPlaying)
            audio.Play();
    }

    void ApplyVolumeToMixer(float volume)
    {
        volume = Mathf.Clamp(volume, 0.001f, 1.0f);
        float dB = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("MusicVolume", dB);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MainMenuScene" && scene.name != "Settings Menu")
        {
            Destroy(gameObject);
        }
    }
}
