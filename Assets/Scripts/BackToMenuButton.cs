using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMenuButton : MonoBehaviour
{
   [SerializeField] Button ExitButton;

    [SerializeField] Image fadeImage;

    [SerializeField] float fadeDuration = 0.01f;

    void Start()
    { 
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            //START FADING
            StartCoroutine(FadeOut());
        }
        if(ExitButton != null)
        {
            ExitButton.onClick.AddListener(OnBackButtonClicked);
        }

    }
    

    public void OnBackButtonClicked()
    {
        LoadSceneWithFade("MainMenuScene");
    }

   public void LoadSceneWithFade(string sceneName)

    {
        StartCoroutine(FadeAndLoad(sceneName));
    }
 IEnumerator FadeAndLoad(string sceneToLoad)
    {
     
        yield return StartCoroutine(FadeIn());

        SceneManager.LoadScene(sceneToLoad);
    }
     IEnumerator FadeIn()
    {

        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0;
            for (float i = 0; i < fadeDuration; i += Time.deltaTime)
            {
                color.a = Mathf.Lerp(0, 1, i / fadeDuration);
                fadeImage.color = color;
                yield return null;
            }
            color.a = 1;
            fadeImage.color = color;

        }
    }

    IEnumerator FadeOut()
    {

        if (fadeImage != null)
        {
            Color color = fadeImage.color;

            color.a = 1;

            for (float i = 0; i < fadeDuration; i += Time.deltaTime)
            {
                color.a = Mathf.Lerp(1, 0, i / fadeDuration);

                fadeImage.color = color;

                yield return null;
            }
            color.a = 0;
            fadeImage.color = color;

        }
    }
}
