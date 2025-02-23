using UnityEngine;
using UnityEngine.UI;

public class VolumeSettingMenu : MonoBehaviour
{
    public Slider soundSlider;
    public Slider musicSlider;

    public float defaultSound = 0.5f;
    public float defaultMusic = 0.5f;

    void Start()
    {
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume", defaultSound);
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", defaultMusic);


        soundSlider.onValueChanged.AddListener(SetSoundVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    public void SetSoundVolume(float volume)
    {
        AudioHandelor.instance.SFXVolume(soundSlider.value);

        PlayerPrefs.SetFloat("soundVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        AudioHandelor.instance.MusicVolume(musicSlider.value);

        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void ResetDefault()
    {
        musicSlider.value = defaultMusic;
        soundSlider.value = defaultSound;

        SetSoundVolume(defaultSound);
        SetMusicVolume(defaultMusic);

        PlayerPrefs.SetFloat("soundVolume", defaultMusic);
        PlayerPrefs.SetFloat("musicVolume", defaultSound);

        PlayerPrefs.Save();
    }

    public void SaveSettings()
    {
        PlayerPrefs.Save();
    }
}
