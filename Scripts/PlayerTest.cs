using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{

    public void TakeDamage(float damage)
    {
        Debug.Log("Le joueur à recus " + damage + " dégats");
    }
}
