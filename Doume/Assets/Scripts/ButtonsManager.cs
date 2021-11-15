using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ButtonsManager : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelOptionsMenu;
    public Dropdown resolutionDropdown;
    public GameObject panelControls;
    public AudioMixer music;

    public void Quitter()
    {
        Application.Quit();
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }


    public void OptionsPanel()
    {
        panelMenu.SetActive(false);

        panelOptionsMenu.SetActive(true);
    }

    public void setMusicVolum(float volume)
    {
        music.SetFloat("Music", volume);
    }

    public void ReturnMenuButton()
    {
        panelMenu.SetActive(true);

        panelOptionsMenu.SetActive(false);

        panelControls.SetActive(false);
    }

    public void ReturnOptionsButton()
    {
        panelMenu.SetActive(false);

        panelOptionsMenu.SetActive(true);

        panelControls.SetActive(false);
    }

    public void ControlPanel()
    {
        panelMenu.SetActive(false);

        panelOptionsMenu.SetActive(false);

        panelControls.SetActive(true);
    }

    public void Fullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void Resolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        Time.timeScale = 1;
    }

}
