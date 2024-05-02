

using TMPro;
using UnityEngine;

// Vincent Lee
// 4/26/24

public class DisplayOptionsMenu : BaseMenu
{
    public TMP_Dropdown windowDropdown, resolutionDropdown;
    private Resolution resolutionBeforeFS;

    public override void Awake()
    {
        base.Awake();
        var iv = PlayerPrefs.GetInt("Window", 0);
        windowDropdown.value = iv;

        if (iv != 0)
        {
            resolutionDropdown.transform.parent.gameObject.SetActive(false);
        }
        
        iv = PlayerPrefs.GetInt("Resolution", 0);
        resolutionDropdown.value = iv;
        
        switch (iv)
        {
            case 0:
                resolutionBeforeFS.width = 1024;
                resolutionBeforeFS.height = 576;
                break;
            case 1:
                resolutionBeforeFS.width = 1280;
                resolutionBeforeFS.height = 720;
                break;
            case 2:
                resolutionBeforeFS.width = 1920;
                resolutionBeforeFS.height = 1080;
                break;
        }
        
        windowDropdown.onValueChanged.AddListener(SetWindowType);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    private void SetWindowType(int value)
    {
        PlayerPrefs.SetInt("Window", value);
        switch (value)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                Screen.SetResolution(resolutionBeforeFS.width, 
                    resolutionBeforeFS.height, FullScreenMode.Windowed);
                resolutionDropdown.transform.parent.gameObject.SetActive(true);
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                Screen.SetResolution(Screen.currentResolution.width, 
                    Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
                resolutionDropdown.transform.parent.gameObject.SetActive(false);
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                Screen.SetResolution(Screen.currentResolution.width, 
                    Screen.currentResolution.height, FullScreenMode.ExclusiveFullScreen);
                resolutionDropdown.transform.parent.gameObject.SetActive(false);
                break;
        }
    }

    private void SetResolution(int value)
    {
        PlayerPrefs.SetInt("Resolution", value);
        switch (value)
        {
            case 0:
                Screen.SetResolution(1024, 576, FullScreenMode.Windowed);
                resolutionBeforeFS.width = 1024;
                resolutionBeforeFS.height = 576;
                break;
            case 1:
                Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
                resolutionBeforeFS.width = 1280;
                resolutionBeforeFS.height = 720;
                break;
            case 2:
                Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
                resolutionBeforeFS.width = 1920;
                resolutionBeforeFS.height = 1080;
                break;
        }
    }
}
