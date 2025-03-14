using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu: MonoBehaviour
{
    [SerializeField] Button StartGameButton;
    [SerializeField] Button ContinueButton;
    [SerializeField] Button ExitButton;
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
    }
    public void StartGame()
    {
        SceneTransitionManager.Instance.LoadSceneWithFade("Chapter1");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
