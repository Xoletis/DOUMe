using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private int health;
    private int armor;

    public int maxHealth = 100;
    public int maxArmor = 100;

    public WeaponStats[] allWeapons;
    [SerializeField]
    private WeaponStats weapon;
    private int i = 0;

    [SerializeField]
    private int weaponReloaderLeft;

    private GunController gunController;

    private void Awake()
    {
        for (int i = 0; i < allWeapons.Length; i++)
        {
            allWeapons[i].munitions = allWeapons[i].maxMunitions;
        }

        weapon = allWeapons[0];
        weaponReloaderLeft = 5;
        health = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        armor = 0;

        gunController = gameObject.GetComponent<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            i--;
            if (i < 0) i = allWeapons.Length - 1;
            weapon = allWeapons[i];
            gunController.changeWeapon();
        }
        if (Input.GetKeyDown("e"))
        {
            i++;
            if (i >= allWeapons.Length) i = 0;
            weapon = allWeapons[i];
            gunController.changeWeapon();
        }
    }

    // Getter des variables
    public int GetHealth()
    {
        return health;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetArmor()
    {
        return armor;
    }
    public int GetMaxArmor()
    {
        return maxArmor;
    }

    // Le joueur perd des points de vie / d'armure
    public void HurtPlayer(int damage)
    {
        if (armor > damage)
            armor -= damage;
        else if (armor < damage)
        {
            health -= (damage - armor);
            armor = 0;
        }
    }

    // Le joueur est-il mort ?
    public bool IsDead()
    {
        return health <= 0;
    }

    public WeaponStats GetWeapon()
    {
        return weapon;
    }

    public int GetWeaponReloaderLeft()
    {
        return weaponReloaderLeft;
    }

    public void AddReloader(int valueToAdd)
    {
        weaponReloaderLeft += valueToAdd;
    }
}