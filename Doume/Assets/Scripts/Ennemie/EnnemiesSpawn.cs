using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesSpawn : MonoBehaviour
{
    public GameObject[] spawners;

    public int nbEnnemies;
    public int spawnerIndex = 0;

    public ActivateAi activateAi;

    private void Start()
    {
        nbEnnemies = spawners.Length;
    }

    //On fait spawn l'ennemie au spawner actuelle
    public void SpawnEnnemies()
    {
        List<GameObject> ennemies = new List<GameObject>();
       
        for (int i = 0; i < LevelManager.instance.levels[PlayerPrefs.GetInt("Level")].ennemieSpawn.Length; i++)
        {
            for (int j = 0; j < LevelManager.instance.levels[PlayerPrefs.GetInt("Level")].ennemieSpawn[i].spawnCount; j++)
            {
                ennemies.Add(LevelManager.instance.levels[PlayerPrefs.GetInt("Level")].ennemieSpawn[i].ennemie);
            }
        }
        int ennemiesIndex = Random.Range(0, ennemies.Count);
        GameObject ennemy =  Instantiate(ennemies[ennemiesIndex],spawners[spawnerIndex].transform.position, Quaternion.identity);
        ennemy.GetComponent<EnnemieAi>().door = this.GetComponent<DestroyRandomDoor>();
        spawnerIndex++;
        ennemy.GetComponent<EnnemieAi>().enabled = false;
        activateAi.ennemies.Add(ennemy);
    }

    //on lance le spawne des ennemies
    public void LaunchEnnemiesSpawn()
    {

        int i = 0;

        while (i < nbEnnemies)
        {
            SpawnEnnemies();
            i++;
        }
    }
}
