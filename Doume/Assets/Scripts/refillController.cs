using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refillController : MonoBehaviour
{
    public enum Type { AllAmmo, Gun, Shotgun, Armor }
    public Type typeToRefill;


    public int nbToAdd = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (typeToRefill)
            {
                case Type.AllAmmo: other.gameObject.GetComponent<PlayerInventory>().AddMunition(nbToAdd);
                    break;
                case Type.Gun: other.gameObject.GetComponent<PlayerInventory>().AddGunAmmo(nbToAdd);
                    break;
                case Type.Shotgun: other.gameObject.GetComponent<PlayerInventory>().AddShotgunAmmo(nbToAdd);
                    break;
                case Type.Armor: other.gameObject.GetComponent<PlayerInventory>().AddArmor(nbToAdd);
                    break;
            }
            Debug.Log("Collison d�tect�e");
            
            Destroy(gameObject);
        }
    }
}
