using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIA : MonoBehaviour
{
    public bool isInvicible;
    public float maxHealth;
    [SerializeField]
    private float health;
    public GameObject blood;
    public int maxCouldown;
    [SerializeField]
    private int timeEnterTowAttaque = 0;
    public int nbCapcity = 0;
    public GameObject lave;

    [HideInInspector]
    public GameObject Porte;

    public string FonctionToHurtPlayer;

    [SerializeField]
    private Animator animator;

    [Header("Fire")]
    public int FireDamage;
    public GameObject FireBullet;
    public Transform[] FireSpawnBullets;

    [Header("Booling")]
    public int BoolingDamage;
    public GameObject Boolingbullet;
    public Transform[] BoolingSpawnBullets;

    [Header("Flash")]
    public int FlashDamage;
    public GameObject FlashBullet;
    public Transform[] FlashSpawnBullets;

    [Header("SpawnEnnemie")]
    public GameObject ennemie;
    public Transform[] EnnemieSpawns;

    public void Start()
    {
        health = maxHealth;
        maxHealth *= PlayerPrefs.GetInt("MultiplyBoss");
        health *= PlayerPrefs.GetInt("MultiplyBoss");
        BoolingDamage *= PlayerPrefs.GetInt("MultiplyBoss");
        FireDamage *= PlayerPrefs.GetInt("MultiplyBoss");
        FlashDamage *= PlayerPrefs.GetInt("MultiplyBoss");
        StartAttaque();
        animator = GetComponent<Animator>();
    }

    public void StartAttaque()
    {
        int n = Random.Range(0, nbCapcity);
        Debug.Log(n);
        switch (n)
        {
            case 0: animator.SetTrigger("Fireing"); break;
            case 1: animator.SetTrigger("Flashing"); break;
            case 2: animator.SetTrigger("Booling"); break;
            case 3: EnnemieSpawn(); break;
            case 4: animator.SetTrigger("Fireing"); break;
            case 5: animator.SetTrigger("Fireing"); break;
            case 6: animator.SetTrigger("Fireing"); break;
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isInvicible)
        {
            Debug.Log("Touché");
            health -= damage;
            //on instantie les particules de sang
            SetBlood();

            if(health <= maxHealth / 2)
            {
                lave.GetComponent<Animator>().SetTrigger("Lave");
            }

            //mort
            if (health <= 0)
            {
                Destroy(lave);

                int n = Random.Range(1, 3);
                if (n == 1)
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().AddMaxHealth(5);
                else
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().stat.multiply += 0.25f;
                
                PlayerPrefs.SetInt("MultiplyBoss", PlayerPrefs.GetInt("MultiplyBoss") + 1);
                Destroy(Porte);
                Destroy(gameObject);
            }
        }
    }

    public void StartCouldown()
    {
        timeEnterTowAttaque = maxCouldown;
        StartCoroutine(Couldown());
    }

    public void SetBlood()
    {
        Instantiate(blood, transform.position, Quaternion.identity);
    }

    public void Fire()
    {
        foreach (Transform spawnBullet in FireSpawnBullets)
        {
            GameObject _bullet = Instantiate(FireBullet, spawnBullet.position, Quaternion.identity);
            _bullet.GetComponent<Bullet>().damage = FireDamage;
            _bullet.GetComponent<Bullet>().playerDamageFonctionName = FonctionToHurtPlayer;
        }

        StartCouldown();
    }

    public void FlashingBullet()
    {
        foreach (Transform spawnBullet in FlashSpawnBullets)
        {
            GameObject _bullet = Instantiate(FlashBullet, spawnBullet.position, Quaternion.identity);
            _bullet.GetComponent<Bullet>().damage = FlashDamage;
            _bullet.GetComponent<Bullet>().playerDamageFonctionName = FonctionToHurtPlayer;
            _bullet.GetComponent<Bullet>().flashing = true;
        }

        StartCouldown();
    }

    public void Booling()
    {
        foreach (Transform spawnBullet in BoolingSpawnBullets)
        {
            GameObject _bullet = Instantiate(Boolingbullet, spawnBullet.position, Quaternion.identity);
            _bullet.GetComponent<Bullet>().damage = BoolingDamage;
            _bullet.GetComponent<Bullet>().playerDamageFonctionName = FonctionToHurtPlayer;
            _bullet.GetComponent<Bullet>().ralentisement = true;
        }

        StartCouldown();
    }

    public void EnnemieSpawn()
    {
        int n = Random.Range(0, 2);
        if (n == 0)
        {
            foreach (Transform ennemieSpawn in EnnemieSpawns)
            {
                Instantiate(ennemie, ennemieSpawn.position, Quaternion.identity);
            }
        }

        StartCouldown();
    }

    public IEnumerator Couldown()
    {
        while(timeEnterTowAttaque > 0)
        {
            yield return new WaitForSeconds(1);
            timeEnterTowAttaque--;
        }
        StartAttaque();
    }
}
