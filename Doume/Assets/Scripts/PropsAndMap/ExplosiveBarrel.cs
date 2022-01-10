using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public ParticleSystem explo;
    public float radius;
    public int dammage;

    public void Explode()
    {
        //ParticleSystem explosion = Instantiate(explo, transform.position, Quaternion.identity);
        //explosion.GetComponent<ParticleSystem>().Play();
        ParticleSystem explosion = Instantiate(explo, transform.position, Quaternion.identity);
        explosion.GetComponent<ParticleSystem>().Play();
        AreaDamageEnemies();
        Destroy(transform.gameObject);
    }

    void AreaDamageEnemies()
    {
        if (PlayerDetected())
        {
            getPlayer().GetComponent<PlayerInventory>().HurtPlayer(dammage);
        }

        foreach(GameObject ennemie in ennemies())
        {
            ennemie.GetComponent<EnnemieAi>().TakeDamage(dammage);
        }
    }


    bool PlayerDetected()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in objects)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    GameObject getPlayer()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in objects)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                return col.gameObject;
            }
        }
        return null;
    }

    List<GameObject> ennemies()
    {
        List<GameObject> _ennemies = new List<GameObject>();

        Collider[] objects = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in objects)
        {
            if (col.gameObject.CompareTag("Ennemie"))
            {
                _ennemies.Add(col.gameObject);
            }
        }
        return _ennemies;
    }
}
