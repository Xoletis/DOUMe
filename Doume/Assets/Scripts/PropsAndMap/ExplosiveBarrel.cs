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

    void AreaDamageEnemies(Vector3 location, float radius, float damage)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
        foreach (Collider col in objectsInRange)
        {
            GameObject go = col.gameObject;
                //linear falloff of effect
                float proximity = (location - go.transform.position).magnitude;
                float effect = 1 - (proximity / radius);

                /*if (go.tag == "Player")
                {
                    go.SendMessage("HurtPlayer", (int)(damage * effect));
                }
                else if (go.tag == "Ennemie")
                {
                    go.SendMessage("TakeDamage", damage * effect);
                }
                else if (go.tag == "ExplosiveBarrel")
                {
                    go.GetComponent<ExplosiveBarrel>().Explode();
                }*/
        }


    }

}
