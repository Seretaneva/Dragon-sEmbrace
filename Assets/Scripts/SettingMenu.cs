using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] Button backButton;
    [SerializeField] string NameOfMainMenuScene;
    [SerializeField] Slider bMSlider;
    [SerializeField] Slider sESlider;
    [SerializeField] Slider vESlider;
    [SerializeField] AudioMixer audioMixer;
    void Start()
    {
        if (backButton != null)
    {
        backButton.onClick.AddListener(GoBackToMainMenu);
    }

    float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
    float effectsVolume = PlayerPrefs.GetFloat("EffectsVolume", 1.0f);
    float voiceVolume = PlayerPrefs.GetFloat("VoiceVolume", 1.0f);

    bMSlider.value = musicVolume;
    sESlider.value = effectsVolume;
    vESlider.value = voiceVolume;

    SetMusicVolume(musicVolume);
    SetEffectsVolume(effectsVolume);
    SetVoiceVolume(voiceVolume);

    }

    public void GoBackToMainMenu()
    {
        SceneTransitionManager.Instance.LoadSceneWithFade(NameOfMainMenuScene);
    }
    public void SetMusicVolume(float value)
{
    value = Mathf.Clamp(value, 0.01f, 1.0f);
    audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
        Debug.Log(Mathf.Log10(value) * 20);
    PlayerPrefs.SetFloat("MusicVolume", value);
}

public void SetEffectsVolume(float value)
{
    value = Mathf.Clamp(value, 0.01f, 1.0f);
    audioMixer.SetFloat("EffectsVolume", Mathf.Log10(value) * 20);

    PlayerPrefs.SetFloat("EffectsVolume", value);
}

public void SetVoiceVolume(float value)
{
    value = Mathf.Clamp(value, 0.01f, 1.0f);
    audioMixer.SetFloat("VoiceVolume", Mathf.Log10(value) * 20);
    PlayerPrefs.SetFloat("VoiceVolume", value);
}

}
