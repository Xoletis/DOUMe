using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float TimeBetweenDeath = 2f;
    public float speed = 1000f;
    public string playerDamageFonctionName;
    public bool flashing;
    public bool ralentisement;
    public bool isBomb;
    public ParticleSystem ExplosedParticule;
    public float ExplosedRange;

    void Start()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 dir = (playerPos - transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(dir * speed, ForceMode.Force);
        StartCoroutine(timeBeforeDistroy());
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            
            if (flashing)
            {
                collision.gameObject.SendMessage("WhitFlash");
            }
            if (ralentisement)
            {
                collision.gameObject.SendMessage("Ralatie");
            }
            if (isBomb)
            {
                Explosed();
            }
            else
            {
                collision.gameObject.SendMessage(playerDamageFonctionName, damage);
            }
            Destroy(gameObject);
        }
    }

    public void Explosed()
    {
        Debug.Log("Boom");
        ParticleSystem explosion = Instantiate(ExplosedParticule, transform.position, Quaternion.identity);
        explosion.GetComponent<ParticleSystem>().Play();
        if (PlayerDetected())
        {
            GameObject.FindGameObjectWithTag("Player").SendMessage(playerDamageFonctionName, damage);
        }
        Destroy(gameObject);
    }

    IEnumerator timeBeforeDistroy()
    {
        yield return new WaitForSeconds(TimeBetweenDeath);
        if (isBomb)
        {
            Explosed();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    bool PlayerDetected()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, ExplosedRange);

        foreach (Collider col in objects)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ExplosedRange);
    }
}
