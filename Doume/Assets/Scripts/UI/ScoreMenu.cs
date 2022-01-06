using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreMenu : MonoBehaviour
{
    public string nameOfGameScene = "GameScene";
    public string nameOfMainMenu = "MainMenu";
    public void Restart()
    {
        PlayerPrefs.SetInt("Score", 0);
        SceneManager.LoadScene(nameOfGameScene);
    }
    public void MainMenu()
    {
        PlayerPrefs.SetInt("Score", 0);
        SceneManager.LoadScene(nameOfMainMenu);
    }
}
