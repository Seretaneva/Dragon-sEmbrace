using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusicFader : MonoBehaviour
{
    [SerializeField] float fadeDuration = 2.0f;
    [SerializeField] float targetVolume = 1f;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
        audioSource.Play();
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(0f, targetVolume, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = targetVolume;
    }
 public IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
         audioSource.volume = 0f;
        audioSource.Stop();
    }
    
}
