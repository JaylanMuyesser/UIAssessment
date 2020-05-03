using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // change scenes
using UnityEngine.Audio; // use audio manager

public class SettingsMenu : MonoBehaviour
{
    public GameObject pressAnyKeyPanel, menuPanel;
    void Start()
    {
        menuPanel.SetActive(false); // will hide menu panel at the start
        ResolutionSetUp();
    }

    void Update()
    {
        if (Input.anyKey && pressAnyKeyPanel.activeSelf) // if we press any input and the press any key panel is still active execute....
        {
            menuPanel.SetActive(true);
            pressAnyKeyPanel.SetActive(false);
        }
    }
    #region Main menu functions
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void ExitToDesktop()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    #endregion
    #region Audio
    public AudioMixer masterAudio;
    public AudioSource audioSource;
    public AudioClip[] clicks;
    public void PlayClip()
    {
        audioSource.clip = clicks[Random.Range(0, clicks.Length)];

    }
    public void ChangeVolume(float volume)
    {
        masterAudio.SetFloat("MasterVolume", volume);
    }
    public void ToggleMute(bool isMuted)
    {
        if (isMuted)
        {
            masterAudio.SetFloat("isMutedVolume", -80);
        }
        masterAudio.SetFloat("isMutedVolume", 0);
    }
    #endregion
    #region Quality
    public void Quality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    #endregion
    #region Resolution
    public void WindowToggle(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    private void ResolutionSetUp()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> resolutionOptions = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            resolutionOptions.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
            resolutionDropdown.AddOptions(resolutionOptions);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    #endregion
}
