
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPauseOptionsAudio : MonoBehaviour
{
    [SerializeField] private GameObject _masterObject;
    [SerializeField] private GameObject _musicObject;
    [SerializeField] private GameObject _sfxObject;
    
    private Slider _masterSlider;
    private Slider _musicSlider;
    private Slider _sfxSlider;
    
    public event UnityAction<float> MasterAudioSet = delegate { };
    public event UnityAction<float> MusicAudioSet = delegate { };
    public event UnityAction<float> SFXAudioSet = delegate { };
    
    private void Awake()
    {
        _masterSlider = _masterObject.GetComponent<Slider>();
        _musicSlider = _musicObject.GetComponent<Slider>();
        _sfxSlider = _sfxObject.GetComponent<Slider>();
    }

    private void Start()
    {
        _masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        _sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    private void OnEnable()
    {
        _masterSlider.onValueChanged.AddListener(SetMasterAudio);
        _musicSlider.onValueChanged.AddListener(SetMusicAudio);
        _sfxSlider.onValueChanged.AddListener(SetSFXAudio);
        _masterSlider.Select();
    }

    private void OnDisable()
    {
        _masterSlider.onValueChanged.RemoveListener(SetMasterAudio);
        _musicSlider.onValueChanged.RemoveListener(SetMusicAudio);
        _sfxSlider.onValueChanged.RemoveListener(SetSFXAudio);
    }

    private void SetMasterAudio(float value)
    {
        MasterAudioSet.Invoke(value);
    }

    private void SetMusicAudio(float value)
    {
        MusicAudioSet.Invoke(value);
    }

    private void SetSFXAudio(float value)
    {
        SFXAudioSet.Invoke(value);
    }
}
