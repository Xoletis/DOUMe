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
        SceneManager.LoadScene(nameOfGameScene);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(nameOfMainMenu);
    }
}
