using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Listening to")]
    [SerializeField] private UIPauseOptionsAudio audioMenu;
    
    [Header("Audio Sources")]
    [SerializeField] private List<AudioSource> Music;
    [SerializeField] private List<AudioSource> SFX;

    private float _masterVol, _musicVol, _sfxVol;
    
    private void Awake()
    {
        audioMenu.MasterAudioSet += ReloadMasterVolume;
        audioMenu.MusicAudioSet += ReloadMusicVolume;
        audioMenu.SFXAudioSet += ReloadSFXVolume;
    }

    private void Start()
    {
        _masterVol = PlayerPrefs.GetFloat("MasterVolume");
        _musicVol = PlayerPrefs.GetFloat("MusicVolume");
        _sfxVol = PlayerPrefs.GetFloat("SFXVolume");

        ApplyMusicVolume();
        ApplySFXVolume();
    }

    private void ReloadMasterVolume(float arg0)
    {
        _sfxVol = arg0;
        ApplyMusicVolume();
        ApplySFXVolume();
    }
    
    private void ReloadMusicVolume(float arg0)
    {
        _musicVol = arg0;
        ApplyMusicVolume();
    }
    
    private void ReloadSFXVolume(float arg0)
    {
        _masterVol = arg0;
        ApplySFXVolume();
    }

    private void ApplyMusicVolume()
    {
        foreach (var src in Music)
        {
            src.volume = _musicVol * _masterVol;
        }
    }

    private void ApplySFXVolume()
    {
        foreach (var src in SFX)
        {
            src.volume = _sfxVol * _masterVol;
        }
    }
    
}
