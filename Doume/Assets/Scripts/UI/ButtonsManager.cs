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
    public GameObject audioWindow;
    public GameObject resolutionWindow;
    public GameObject commandeWindow;
    public AudioMixer music;
    public Slider musicSlider;
    public Slider soundSlider;
    public Slider SensitivityX;
    public Slider SensitivityY;
    public Toggle fullScreenToggle;
    public bool isMenu = false;
    public Button resolutin, audio, commande;

    public void Quitter()
    {
        Application.Quit();
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.LeftControl) && isMenu)
        {
            SceneManager.LoadScene("Demo");
        }
    }
    public void audioWindowActive()
    {
        audioWindow.SetActive(true);
        audio.interactable = false;
        resolutin.interactable = true;
        commande.interactable = true;
        resolutionWindow.SetActive(false);
        commandeWindow.SetActive(false);
    }

    public void resolutionWindowActive()
    {
        audioWindow.SetActive(false);
        resolutionWindow.SetActive(true);
        commandeWindow.SetActive(false);
        audio.interactable = true;
        resolutin.interactable = false;
        commande.interactable = true;
    }

    public void commandWindowActive()
    {
        audioWindow.SetActive(false);
        resolutionWindow.SetActive(false);
        commandeWindow.SetActive(true);
        audio.interactable = true;
        resolutin.interactable = true;
        commande.interactable = false;
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

    public void immortel()
    {
        /*if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt))
            SceneManager.LoadScene("LevelInfinieImortal");

        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Space))*/
            
    }

    public void setSensivityX(float sensitivity)
    {
        PlayerPrefs.SetFloat("SensivityX", sensitivity);
    }

    public void setSensivityY(float sensitivity)
    {
        PlayerPrefs.SetFloat("SensivityY", sensitivity);
    }

    public void setSoundVolum(float volume)
    {
        music.SetFloat("Sound", volume);
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
        audio.interactable = false;
        fullScreenToggle.isOn = Screen.fullScreen;
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
        float n;
        music.GetFloat("Music", out n);
        musicSlider.value = n;
        music.GetFloat("Sound", out n);
        soundSlider.value = n;
        if(PlayerPrefs.GetFloat("SensivityX") == 0)
        {
            PlayerPrefs.SetFloat("SensivityX", 5);
        }
        if (PlayerPrefs.GetFloat("SensivityY") == 0)
        {
            PlayerPrefs.SetFloat("SensivityY", 5);
        }
        SensitivityX.value = PlayerPrefs.GetFloat("SensivityX");
        SensitivityY.value = PlayerPrefs.GetFloat("SensivityY");
    }

}
