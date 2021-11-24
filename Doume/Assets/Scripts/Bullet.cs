using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float damage;
    public float TimeBetweenDeath = 2f;
    public float speed = 1000f;
    [HideInInspector]
    public string playerDamageFonctionName;

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
            collision.gameObject.SendMessage(playerDamageFonctionName, damage);
            Destroy(gameObject);
        }
    }

    IEnumerator timeBeforeDistroy()
    {
        yield return new WaitForSeconds(TimeBetweenDeath);
        Destroy(gameObject);
    }
}
