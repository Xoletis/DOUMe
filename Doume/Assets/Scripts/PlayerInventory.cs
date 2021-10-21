using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int health;
    private int armor;

    public int maxHealth = 100;
    public int maxArmor = 100;

    public WeaponStats[] allWeapons;
    private WeaponStats weapon;

    [SerializeField]
    private int weaponReloaderLeft;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        armor = 0;
        weapon = allWeapons[0];
        weaponReloaderLeft = 5;
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