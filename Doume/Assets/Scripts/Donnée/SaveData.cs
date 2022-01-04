using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    
    PlayerInventory player;

    //Paterne Singelton
    public static SaveData instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de save data");
        }

        instance = this;
    }

    //Recuperation des Stats du joueur
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public void Save()
    {
        //on recupere les stats et on les met au format JSON
        string inventoryData = JsonUtility.ToJson(player.stat);
        //on recupere le chemain o� enregistrer le fichier de sauvgarde (Application.persistentDataPath = chemin de l'application)
        string fillPath = Application.persistentDataPath + "/Data.json";
        //On cr�er le fichier avec les stats, si le fichier existe on le suprimme pour le recr��.
        System.IO.File.WriteAllText(fillPath, inventoryData);
        Debug.Log("Sauvgard effectu�");
    }

    public void Load()
    {
        //on recupere le chemain o� enregistrer le fichier de sauvgarde (Application.persistentDataPath = chemin de l'application)
        string fillPath = Application.persistentDataPath + "/Data.json";
        //on verifi si le fichier de sauvgard existe
        if (System.IO.File.Exists(fillPath))
        {
            //On lit le fichier de sauvgarde
            string inventoryData = System.IO.File.ReadAllText(fillPath);

            //On apllique la sauvgard aux stats
            player.stat = JsonUtility.FromJson<Stat>(inventoryData);
            //refreshe des donn�es sur l'�cran
            player.refreshscreen();
            Debug.Log("chargement effectu�");
        }
    }
}
