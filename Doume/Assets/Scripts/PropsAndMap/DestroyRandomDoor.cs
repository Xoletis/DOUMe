using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRandomDoor : MonoBehaviour
{
    public int doorRandom;
    [SerializeField]
    List<GameObject> doors;
    bool isDestroyed = false;
    public int ennemies = 0;

    void Start()
    {
        //Quelle porte sera suprimer
        doorRandom = Random.Range(0, doors.Count);
    }
    public void AddEnnemy()
    {
        ennemies++;
    }

    //On enleve un ennemie de la sale
    public void DestroyEnnemy()
    {
        ennemies--;
        //si il n'y a plus d'ennemeie dans la sale on supprime une porte alléatoire.
        if (ennemies <= 0)
        {

            ennemies = 0;
            if (isDestroyed == false)
            {
                doors[doorRandom].GetComponent<spawnNextRoom>().spawnNextEnnemy();
                isDestroyed = !isDestroyed;
            }
        }
    }
}
