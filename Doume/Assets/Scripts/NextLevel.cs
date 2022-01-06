using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            if(LevelManager.instance.levels.Length < PlayerPrefs.GetInt("Level"))
            {
                PlayerPrefs.SetInt("Level", 0);
                PlayerPrefs.SetInt("MultiplyEnnemie", PlayerPrefs.GetInt("MultiplyEnnemie") + 1);
            }
            SaveData.instance.Save();
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
