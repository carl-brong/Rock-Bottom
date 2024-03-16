using UnityEngine;

public class UIDataHandler
{
    public void SetDifficulty(int value)
    {
        PlayerPrefs.SetInt("Difficulty", value);
    }

    public void SetWindow(int value)
    {
        PlayerPrefs.SetInt("Window", value);
    }

    public void SetResolution(int value)
    {
        PlayerPrefs.SetInt("Resolution", value);
    }

    public void SetMasterAudio(float value)
    {
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void SetMusicAudio(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetSFXAudio(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}