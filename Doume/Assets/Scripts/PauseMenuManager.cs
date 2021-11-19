using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject canvasPause;
    [SerializeField]
    public bool isPaused = false;


    public static PauseMenuManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Il y a deux instances de PauseMenuManger dans la scene");
        }

        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            canvasPause.SetActive(isPaused);
            if (isPaused)
            {
                Time.timeScale = 0;
            }else if (!isPaused)
            {
                Time.timeScale = 1;
            }
        }

        Cursor.visible = isPaused;
    }

    public void ReloadLevel()
    {
        Scene actualScene = SceneManager.GetActiveScene();
        SceneManager.UnloadScene(actualScene.name);
        SceneManager.LoadScene(actualScene.name);
    }

    public void ContinueGame()
    {
        isPaused = !isPaused;
        canvasPause.SetActive(isPaused);
        Time.timeScale = 1;
    }
}
