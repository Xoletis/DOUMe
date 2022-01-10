using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refillController : MonoBehaviour
{
    public enum Type { AllAmmo, Gun, Shotgun, Energy, Armor, Health, HealthInTime, MaxHealth, Dammage }
    public Type typeToRefill;


    public float nbToAdd = 5;

    //On ajout les stats au joueur
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (typeToRefill)
            {
                case Type.AllAmmo: other.gameObject.GetComponent<PlayerInventory>().AddMunition((int)nbToAdd);
                    break;
                case Type.Gun: other.gameObject.GetComponent<PlayerInventory>().AddGunAmmo((int)nbToAdd);
                    break;
                case Type.Shotgun: other.gameObject.GetComponent<PlayerInventory>().AddShotgunAmmo((int)nbToAdd);
                    break;
                case Type.Energy: other.gameObject.GetComponent<PlayerInventory>().AddEnergyAmmo((int)nbToAdd);
                    break;
                case Type.Armor: other.gameObject.GetComponent<PlayerInventory>().AddArmor((int)nbToAdd);
                    break;
                case Type.Health: other.gameObject.GetComponent<PlayerInventory>().AddHealth((int)nbToAdd);
                    break;
                case Type.HealthInTime: other.gameObject.GetComponent<PlayerInventory>().TimeHealing((int)nbToAdd);
                    break;
                case Type.MaxHealth: other.gameObject.GetComponent<PlayerInventory>().AddMaxHealth((int)nbToAdd);
                    break;
                case Type.Dammage: other.gameObject.GetComponent<PlayerInventory>().dammageplus(nbToAdd);
                    break;
            }
            Debug.Log("Collison détectée");
            
            Destroy(gameObject);
        }
    }
}
