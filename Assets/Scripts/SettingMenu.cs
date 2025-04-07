using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider effectsSlider;
    [SerializeField] Slider voiceSlider;
    [SerializeField] AudioMixer audioMixer;
    void Start()
    {
    float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
    float effectsVolume = PlayerPrefs.GetFloat("EffectsVolume", 1.0f);
    float voiceVolume = PlayerPrefs.GetFloat("VoiceVolume", 1.0f);

    float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
    musicSlider.value = savedVolume;
    effectsSlider.value = effectsVolume;
    voiceSlider.value = voiceVolume;

   
    SetMusicVolume(savedVolume);
    SetEffectsVolume(effectsVolume);
    SetVoiceVolume(voiceVolume);

    }
public void SetMusicVolume(float value)
{
    value = Mathf.Clamp(value, 0.001f, 1.0f);
    float dB = Mathf.Log10(value) * 20;
    audioMixer.SetFloat("MusicVolume", dB);
    PlayerPrefs.SetFloat("MusicVolume", value);
    PlayerPrefs.Save();
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
