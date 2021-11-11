using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRandomDoor : MonoBehaviour
{
    private int doorRandom;
    [SerializeField]
    List<GameObject> doors;
    bool isDestroyed = false;
    public int ennemies = 10;
    // Start is called before the first frame update
    void Start()
    {
        doorRandom = Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddEnnemy();
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            DestroyEnnemy();
        }


        if (ennemies <= 0)
        {
            ennemies = 0;
            if (isDestroyed == false)
            {
                Destroy(doors[doorRandom]);
                isDestroyed = !isDestroyed;
            }
        }
    }

    void AddEnnemy()
    {
        ennemies++;
    }

    void DestroyEnnemy()
    {
        ennemies--;
    }
}
