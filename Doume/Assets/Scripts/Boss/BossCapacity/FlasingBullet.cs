using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlasingBullet : BossCapacity
{
    public int damage;
    public GameObject bullet;
    public Transform[] spawnBullets;
    public string FonctionToHurtPlayer;

    override public void Attack()
    {
        animator.SetTrigger(Annime);
    }

    public void flashing()
    {
        foreach (Transform spawnBullet in spawnBullets)
        {
            GameObject _bullet = Instantiate(bullet, spawnBullet.position, Quaternion.identity);
            _bullet.GetComponent<Bullet>().damage = damage;
            _bullet.GetComponent<Bullet>().playerDamageFonctionName = FonctionToHurtPlayer;
            _bullet.GetComponent<Bullet>().flashing = true;
        }
    }

    public void flashingActive()
    {
        Active();
    }
}
