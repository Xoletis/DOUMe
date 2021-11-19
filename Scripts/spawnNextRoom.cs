using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnNextRoom : MonoBehaviour
{
    public EnnemiesSpawn ennemiesSpawn;

    public void spawnNextEnnemy()
    {
        if(ennemiesSpawn != null)
            ennemiesSpawn.LaunchEnnemiesSpawn();
        Destroy(gameObject);
    }
}
