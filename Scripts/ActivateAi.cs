using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAi : MonoBehaviour
{
    public List<GameObject> ennemies;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < ennemies.Count; i++)
            {
                ennemies[i].GetComponent<EnnemieAi>().enabled = true;
            }
        }
        Destroy(gameObject);
    }
}
