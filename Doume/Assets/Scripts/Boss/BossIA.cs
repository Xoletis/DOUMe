using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIA : MonoBehaviour
{
    [SerializeField]
    public BossCapacity[] capacity;
    public bool isInvicible;
    public float health;
    public GameObject blood;
    public int maxCouldown;
    [SerializeField]
    private int timeEnterTowAttaque = 0;

    public void Start()
    {
        StartAttaque();
    }

    public void StartAttaque()
    {
        int n = Random.Range(0, capacity.Length);
        capacity[n].Active();
        timeEnterTowAttaque = maxCouldown;
    }

    public void TakeDamage(float damage)
    {
        if (!isInvicible)
        {
            Debug.Log("Touché");
            health -= damage;
            //on instantie les particules de sang
            SetBlood();

            //mort
            if (health <= 0)
            {
                Debug.Log("Mort");
            }
        }
    }

    public void StartCouldown()
    {
        StartCoroutine(Couldown());
    }

    public void SetBlood()
    {
        Instantiate(blood, transform.position, Quaternion.identity);
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
