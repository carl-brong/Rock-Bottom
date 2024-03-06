using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ResolutionSetter : MonoBehaviour
{
    [SerializeField] private GameObject _windowObject, _resolutionObject;

    private TMP_Dropdown _windowOption, _resolutionOption;
    private Resolution _max_resolution;
    
    private void Awake()
    {
        _windowOption = _windowObject.GetComponent<TMP_Dropdown>();
        _resolutionOption = _resolutionObject.GetComponent<TMP_Dropdown>();
        _max_resolution = (Resolution)Screen.resolutions.GetValue(Screen.resolutions.Length - 1);
    }

    private void SetFullScreen()
    {
        Screen.SetResolution(_max_resolution.width, _max_resolution.height, FullScreenMode.ExclusiveFullScreen);
    }

    private void SetFullScreenWindowed()
    {
        Screen.SetResolution(_max_resolution.width, _max_resolution.height, FullScreenMode.FullScreenWindow);
    }

    private void SetWindowed(int w, int h)
    {
        Screen.SetResolution(w, h, FullScreenMode.Windowed);
    }

    public void SetScreen()
    {
        var value = _windowOption.value;
        var res = _resolutionOption.value;
        switch (value)
        {
            case 0:
                switch (res)
                {
                    case 0:
                        SetWindowed(1024, 576);
                        break;
                    case 1:
                        SetWindowed(1280, 720);
                        break;
                    case 2:
                        SetWindowed(1920, 1080);
                        break;
                }
                _resolutionObject.SetActive(true);
                break;
            case 1:
                SetFullScreenWindowed();
                _resolutionObject.SetActive(false);
                break;
            case 2:
                SetFullScreen();
                _resolutionObject.SetActive(false);
                break;
        }
    }

    public void SetScreenResolution()
    {
        var value = _resolutionOption.value;
        switch (value)
        {
            case 0:
                Screen.SetResolution(1024, 576, FullScreenMode.Windowed);
                break;
            case 1:
                Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
                break;
        }
    }
    
}
