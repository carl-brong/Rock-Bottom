
using UnityEngine;
using UnityEngine.Audio;

// Vincent Lee
// 4/26/24

public class AudioSingleton : MonoBehaviour
{
    public static AudioSingleton Instance { get; private set; }
    [SerializeField] private AudioMixer _mixer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            if (Instance.transform.parent != null)
                transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume", 0));
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0));
        SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 0));
    }

    public void SetMasterVolume(float value)
    {
        PlayerPrefs.SetFloat("MasterVolume", value);
        _mixer.SetFloat("MasterVolume", (value != 0) 
            ? ConvertToDb(value) 
            : -80);
    }
    
    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        _mixer.SetFloat("MusicVolume", (value != 0) 
            ? ConvertToDb(value) 
            : -80);
    }
    
    public void SetSFXVolume(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        _mixer.SetFloat("SFXVolume", (value != 0) 
            ? ConvertToDb(value) 
            : -80);
    }
    
    private static float ConvertToDb(float value)
    {
        return 20 * Mathf.Log10(value);
    }
}
