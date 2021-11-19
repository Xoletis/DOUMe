using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesSpawn : MonoBehaviour
{
    public GameObject[] ennemies;
    public GameObject[] spawners;

    public int nbEnnemies;
    public int spawnerIndex = 0;

    public ActivateAi activateAi;

    private void Start()
    {
        nbEnnemies = spawners.Length;
    }

    public void SpawnEnnemies()
    {
        int ennemiesIndex = Random.Range(0, ennemies.Length);
        GameObject ennemy =  Instantiate(ennemies[ennemiesIndex],spawners[spawnerIndex].transform.position, Quaternion.identity);
        ennemy.GetComponent<LookingAtCamera>().player = GameObject.FindGameObjectWithTag("Player");
        ennemy.GetComponent<EnnemieAi>().door = this.GetComponent<DestroyRandomDoor>();
        spawnerIndex++;
        ennemy.GetComponent<EnnemieAi>().enabled = false;
        activateAi.ennemies.Add(ennemy);
    }

    public void LaunchEnnemiesSpawn()
    {

        int i = 0;

        while (i < nbEnnemies)
        {
            Debug.Log("Ok");
            SpawnEnnemies();
            i++;
        }
    }
}
