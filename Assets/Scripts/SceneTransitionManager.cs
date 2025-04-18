using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    [SerializeField] Image fadeImage;
    [SerializeField] TMP_Text text;
    [SerializeField] float fadeDuration = 0.5f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            //START FADING
            StartCoroutine(FadeOut(null));
        }
        if (text != null)
        {
            text.gameObject.SetActive(true);
            text.raycastTarget = false;
            //START FADING
            StartCoroutine(TextFadeOut(null));
        }
    }


    public void LoadSceneWithFade(string sceneName)

    {
        StartCoroutine(FadeAndLoad(sceneName));
    }

    IEnumerator FadeAndLoad(string sceneToLoad)
    {

        BackgroundMusicFader musicFader = Object.FindFirstObjectByType<BackgroundMusicFader>();
        if (musicFader != null)
        {
            yield return StartCoroutine(musicFader.FadeOut());
        }

         
       bool fadeInDone = false;
    bool textFadeInDone = false;

    // Rulează simultan
    StartCoroutine(FadeIn(() => fadeInDone = true));
    StartCoroutine(TextFadeIn(() => textFadeInDone = true));

    // Așteaptă până când ambele sunt gata
    yield return new WaitUntil(() => fadeInDone && textFadeInDone);
       
        // Load the new scene
        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator FadeIn(System.Action onComplete)
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
         onComplete?.Invoke();
    }

    IEnumerator FadeOut(System.Action onComplete)
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
         onComplete?.Invoke();
    }
    IEnumerator TextFadeIn(System.Action onComplete)
    {

        if (text != null)
        {
            Color color1 = text.color;
            color1.a = 0;

            for (float i = 0; i < fadeDuration; i += Time.deltaTime)
            {

                color1.a = Mathf.Lerp(0, 1, i / fadeDuration);
                text.color = color1;

                yield return null;
            }

            text.color = color1;
        }
         onComplete?.Invoke();
    }
    IEnumerator TextFadeOut(System.Action onComplete)
    {

        if (text != null)
        {

            Color color1 = text.color;

            color1.a = 1;

            for (float i = 0; i < fadeDuration; i += Time.deltaTime)
            {

                color1.a = Mathf.Lerp(1, 0, i / fadeDuration);

                text.color = color1;
                yield return null;
            }

            color1.a = 0;
            text.color = color1;
        }
         onComplete?.Invoke();
    }
}
