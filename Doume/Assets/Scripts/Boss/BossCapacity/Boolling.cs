using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boolling : BossCapacity
{
    public int damage;
    public GameObject bullet;
    public Transform[] spawnBullets;
    public string FonctionToHurtPlayer;

    override public void Attack()
    {
        animator.SetTrigger(Annime);
    }

    public void StartBooling()
    {
        foreach (Transform spawnBullet in spawnBullets)
        {
            GameObject _bullet = Instantiate(bullet, spawnBullet.position, Quaternion.identity);
            _bullet.GetComponent<Bullet>().damage = damage;
            _bullet.GetComponent<Bullet>().playerDamageFonctionName = FonctionToHurtPlayer;
            _bullet.GetComponent<Bullet>().ralentisement = true;
            //Ralentire
        }
    }

    public void BoolingActive()
    {
        Active();
    }
}
