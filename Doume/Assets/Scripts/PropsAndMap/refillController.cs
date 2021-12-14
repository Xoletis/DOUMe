using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refillController : MonoBehaviour
{
    public enum Type { AllAmmo, Gun, Shotgun, Energy, Armor, Health, HealthInTime, MaxHealth }
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
                case Type.Energy: other.gameObject.GetComponent<PlayerInventory>().AddEnergyAmmo(nbToAdd);
                    break;
                case Type.Armor: other.gameObject.GetComponent<PlayerInventory>().AddArmor(nbToAdd);
                    break;
                case Type.Health: other.gameObject.GetComponent<PlayerInventory>().AddHealth(nbToAdd);
                    break;
                case Type.HealthInTime: other.gameObject.GetComponent<PlayerInventory>().TimeHealing(nbToAdd);
                    break;
                case Type.MaxHealth: other.gameObject.GetComponent<PlayerInventory>().AddMaxHealth(nbToAdd);
                    break;
            }
            Debug.Log("Collison détectée");
            
            Destroy(gameObject);
        }
    }
}
