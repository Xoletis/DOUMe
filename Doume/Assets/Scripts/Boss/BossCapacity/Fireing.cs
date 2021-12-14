using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireing : BossCapacity
{

    public int damage;
    public GameObject bullet;
    public Transform[] spawnBullets;
    public string FonctionToHurtPlayer;
    

    override public void Attack()
    {
        animator.SetTrigger(Annime);
    }

    public void StartFiering()
    {
        foreach(Transform spawnBullet in spawnBullets)
        {
            GameObject _bullet = Instantiate(bullet, spawnBullet.position, Quaternion.identity);
            _bullet.GetComponent<Bullet>().damage = damage;
            _bullet.GetComponent<Bullet>().playerDamageFonctionName = FonctionToHurtPlayer;
        }
    }

    public void FireingActive()
    {
        Active();
    }
}
