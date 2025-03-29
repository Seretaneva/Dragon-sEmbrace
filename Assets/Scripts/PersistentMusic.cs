using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;

    void Awake()
    {
        string scene = SceneManager.GetActiveScene().name;

        // Dacă NU suntem în MainMenu sau Settings, distrugem imediat
        if (scene != "MainMenuScene" && scene != "Settings Menu")
        {
            Destroy(gameObject);
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

    void Start()
    {
        // Pornește muzica dacă nu e deja pornită
        var audio = GetComponent<AudioSource>();
        if (!audio.isPlaying)
        {
            audio.Play();
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
