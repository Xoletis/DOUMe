using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refillController : MonoBehaviour
{
    public enum Type { AllAmmo, Gun, Shotgun }
    public Type typeToRefill;


    public int nbMunitonToAdd = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (typeToRefill)
            {
                case Type.AllAmmo: other.gameObject.GetComponent<PlayerInventory>().AddMunition(nbMunitonToAdd);
                    break;
                case Type.Gun: other.gameObject.GetComponent<PlayerInventory>().AddGunAmmo(nbMunitonToAdd);
                    break;
                case Type.Shotgun: other.gameObject.GetComponent<PlayerInventory>().AddShotgunAmmo(nbMunitonToAdd);
                    break;
            }
            Debug.Log("Collison détectée");
            
            Destroy(gameObject);
        }
    }
}
