using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refillController : MonoBehaviour
{
    public enum Type { AllAmmo, Gun, Shotgun, Armor, Health }
    public Type typeToRefill;


    public int nbToAdd = 5;

    //On ajout les stats au joueur
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
                case Type.Health: other.gameObject.GetComponent<PlayerInventory>().AddHealth(nbToAdd);
                    break;
            }
            Debug.Log("Collison détectée");
            
            Destroy(gameObject);
        }
    }
}
