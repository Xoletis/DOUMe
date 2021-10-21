using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     public GameObject player;
    private PlayerInventory inventory;
    [SerializeField]
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        inventory = player.GetComponent<PlayerInventory>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            inventory.HurtPlayer(20);
        }

        if (inventory.GetHealth() <= 0)
        {
            Debug.Log("mort du joueur");
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
