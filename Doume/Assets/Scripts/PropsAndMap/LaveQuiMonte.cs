using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaveQuiMonte : MonoBehaviour
{

    public GameObject boss;

    public void DesactiveAI()
    {
        boss.SetActive(false);
    }

    public void ActiveAi()
    {
        boss.SetActive(true);
    }
}
