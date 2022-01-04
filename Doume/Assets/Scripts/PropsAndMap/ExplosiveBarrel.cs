using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public ParticleSystem explo;

    public void Explode()
    {
        //ParticleSystem explosion = Instantiate(explo, transform.position, Quaternion.identity);
        //explosion.GetComponent<ParticleSystem>().Play();
        ParticleSystem explosion = Instantiate(explo, transform.position, Quaternion.identity);
        explosion.GetComponent<ParticleSystem>().Play();
        AreaDamageEnemies(transform.position, 2, 20);
        Destroy(transform.gameObject);
    }

    void AreaDamageEnemies(Vector3 location, float radius, int damage)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
        foreach (Collider col in objectsInRange)
        {
            GameObject go = col.gameObject;
                //linear falloff of effect
                float proximity = (location - go.transform.position).magnitude;

                if (go.CompareTag("Player"))
                {
                    go.SendMessage("HurtPlayer", damage);
                }
                else if (go.CompareTag("Ennemie"))
                {
                    go.SendMessage("TakeDamage", damage);
                }
                else if (go.CompareTag("ExplosiveBarrel"))
                {
                    go.GetComponent<ExplosiveBarrel>().Explode();
                }
        }


    }

}
