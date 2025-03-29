using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu: MonoBehaviour
{
    [SerializeField] Button StartGameButton;
    [SerializeField] Button ContinueButton;
    [SerializeField] Button SettingsButton;
    [SerializeField] Button ExitButton;
    [SerializeField] string StartScene;
     [SerializeField] string NameOfSettingsMenuScene;
    void Start()
    {
        if(StartGameButton != null)
        {
            StartGameButton.onClick.AddListener(StartGame);
        }
        if(ExitButton != null)
        {
            ExitButton.onClick.AddListener(ExitGame);
        }
        if(SettingsButton != null)
        {
            SettingsButton.onClick.AddListener(GoToSettingsMenu);
        }
    }
    public void StartGame()
    {
        SceneTransitionManager.Instance.LoadSceneWithFade(StartScene);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToSettingsMenu()
    {
        SceneTransitionManager.Instance.LoadSceneWithFade(NameOfSettingsMenuScene);
    }
}
