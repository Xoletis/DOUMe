using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaveQuiMonte : MonoBehaviour
{

    public GameObject boss;
    public GameObject porte;

    public void DesactiveAI()
    {
        boss.SetActive(false);
        porte.SetActive(true);
    }

    public void ActiveAi()
    {
        boss.SetActive(true);
        boss.GetComponent<BossIA>().Porte = porte;
        boss.GetComponent<BossIA>().StartAttaque();
    }
}
