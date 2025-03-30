using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;

    public static object Instance { get; internal set; }
    

    void Start()
    {
        // Pornește muzica dacă nu e deja pornită
        var audio = GetComponent<AudioSource>();
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        Debug.Log(musicVolume);
        audio.volume = musicVolume;
       
        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }
    void Awake()
    {
        string scene = SceneManager.GetActiveScene().name;

        // Dacă NU suntem în MainMenu sau Settings, distrugem imediat
        if (scene != "MainMenuScene" && scene != "Settings Menu")
        {
            Destroy(gameObject);
            Debug.Log("S-a distrus muzica din menu");
            return;
        }

        // Singleton logic – păstrăm o singură instanță
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

   

    void Update()
    {
        string scene = SceneManager.GetActiveScene().name;

        // Dacă am trecut din MainMenu/Settings în altă scenă, oprim muzica
        if (scene != "MainMenuScene" && scene != "Settings Menu")
        {
            Destroy(gameObject);
        }
    }
}
