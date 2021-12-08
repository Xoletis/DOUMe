using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    public string nameOfMainMenu = "MainMenu";

    public void MainMenu()
    {
        SceneManager.LoadScene(nameOfMainMenu);
    }
}
