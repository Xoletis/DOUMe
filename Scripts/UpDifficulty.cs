using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDifficulty : MonoBehaviour
{
    public EnnemieData[] ennemieDatas;

    private int lvl = 0;

    public float damageMultiplier, healthMultyplier, speedMultiplier, attackSpeedMuliplier, viewAeraMultyplier, attackAeraMultyplier;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Up();
        }
    }

    public void Up()
    {
        foreach (EnnemieData data in ennemieDatas)
        {
            data.damage += damageMultiplier;
            data.health += healthMultyplier;
            data.speed += speedMultiplier;
            data.viewArea += viewAeraMultyplier;
            data.attackRange += attackAeraMultyplier;
            data.attackCouldown -= attackSpeedMuliplier;
            lvl++;
        }
    }

    private void OnApplicationQuit()
    {
        foreach (EnnemieData data in ennemieDatas)
        {
            data.damage -= damageMultiplier * lvl;
            data.health -= healthMultyplier * lvl;
            data.speed -= speedMultiplier * lvl;
            data.viewArea -= viewAeraMultyplier * lvl;
            data.attackRange -= attackAeraMultyplier * lvl;
            data.attackCouldown += attackSpeedMuliplier * lvl;
        }
    }
}
