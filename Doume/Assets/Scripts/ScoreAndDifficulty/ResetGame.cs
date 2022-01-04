using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Level", 0);
    }
}
