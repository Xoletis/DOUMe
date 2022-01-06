using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.SetInt("MultiplyBoss", 1);
        PlayerPrefs.SetInt("MultiplyEnnemie", 1);
        string fillPath = Application.persistentDataPath + "/Data.json";
        Debug.Log(fillPath);
        System.IO.File.Delete(fillPath);
    }
}
