

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Vincent Lee
// 4/26/24

public class AudioMenu : BaseMenu
{
    public Slider masterSlider, musicSlider, sfxSlider;
    public AudioMixer mixer;

    public override void Awake()
    {
        base.Awake();

        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        masterSlider.onValueChanged.AddListener(x => AudioSingleton.Instance.SetMasterVolume(x));
        
        musicSlider.onValueChanged.AddListener(x => AudioSingleton.Instance.SetMusicVolume(x));
        
        sfxSlider.onValueChanged.AddListener(x => AudioSingleton.Instance.SetSFXVolume(x));
    }
}
