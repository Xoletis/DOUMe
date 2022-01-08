using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public spawnByLevel[] levels;

    // Start is called before the first frame update
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Il y a déjat une instance de LevelManager dans la scene");
        }

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


[System.Serializable]
public struct spawnByLevel
{
    public ennemieSpawn[] ennemieSpawn;
}

[System.Serializable]
public struct ennemieSpawn
{
    public GameObject ennemie;
    public float spawnCount;
}
