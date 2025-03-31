using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenuFromScene : MonoBehaviour
{
    [SerializeField] Button backButton;
    [SerializeField] Button settingsButton;
    [SerializeField] GameObject settingPanel;

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider effectsSlider;
    [SerializeField] Slider voiceSlider;

    [SerializeField] AudioMixer audioMixer;

    void Start()
    {
        settingPanel.SetActive(false);

        // Activare butoane
        if (backButton != null)
            backButton.onClick.AddListener(HideSettingsPanel);

        if (settingsButton != null)
            settingsButton.onClick.AddListener(ShowSettingsPanel);

        // Setări volum salvate
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        float effectsVolume = PlayerPrefs.GetFloat("EffectsVolume", 1.0f);
        float voiceVolume = PlayerPrefs.GetFloat("VoiceVolume", 1.0f);

        musicSlider.value = musicVolume;
        effectsSlider.value = effectsVolume;
        voiceSlider.value = voiceVolume;

        SetMusicVolume(musicVolume);
        SetEffectsVolume(effectsVolume);
        SetVoiceVolume(voiceVolume);
    }

    public void SetMusicVolume(float value)
    {
        value = Mathf.Clamp(value, 0.01f, 1.0f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
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

    // Afișează panelul cu setări
    void ShowSettingsPanel()
    {
        settingPanel.SetActive(true);
    }

    // Ascunde panelul cu setări
    void HideSettingsPanel()
    {
        settingPanel.SetActive(false);
    }
}
