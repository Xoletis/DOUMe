using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refillController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            Debug.Log("Collison d�tect�e");
            other.gameObject.GetComponent<PlayerInventory>().AddMunition(1);
            Destroy(gameObject);
    }
}
