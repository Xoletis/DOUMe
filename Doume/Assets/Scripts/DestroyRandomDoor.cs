using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRandomDoor : MonoBehaviour
{
    private int doorRandom;
    [SerializeField]
    List<GameObject> doors;
    bool isDestroyed = false;
    public int ennemies = 0;
    // Start is called before the first frame update
    void Start()
    {
        doorRandom = Random.Range(0, doors.Count);
    }
    public void AddEnnemy()
    {
        ennemies++;
    }

    public void DestroyEnnemy()
    {
        ennemies--;
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
