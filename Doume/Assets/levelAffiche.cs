using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelAffiche : MonoBehaviour
{
    public UnityEngine.UI.Text leveltext;

    void Start()
    {
        leveltext.text = "Etage : " + PlayerPrefs.GetInt("RealLevel");
    }
}
