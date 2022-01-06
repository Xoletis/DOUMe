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


    }

}
