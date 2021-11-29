using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int armor;

    public int maxHealth = 100;
    public int maxArmor = 100;

    public WeaponStats[] allWeapons;
    private WeaponStats weapon;
    private int i = 0;

    public int ShotgunAmmo;
    public int GunAmmo;
    private int ShotgunAmmoMax = 50;
    private int GunAmmoMax = 10000;

    private GunController gunController;

    public Text textVie;
    public Text textArmor;

    public Text shotgunAmmoText;
    public Text gunAmmoText;
    public Text shotgunAmmoMaxText;
    public Text gunAmmoMaxText;

    private void Awake()
    {
        for (int i = 0; i < allWeapons.Length; i++)
        {
            allWeapons[i].munitions = allWeapons[i].maxMunitions;
        }

        weapon = allWeapons[0];
        health = maxHealth;
        GunAmmo = GunAmmoMax;
        ShotgunAmmo = ShotgunAmmoMax;
        refreshscreen();
    }

    // Start is called before the first frame update
    void Start()
    {
        armor = 0;

        gunController = gameObject.GetComponent<GunController>();
        textVie.text = (health * 100) / maxHealth + "%";
        textArmor.text = (armor * 100) / maxArmor + "%";
    }

    void Update()
    {
        if (Input.GetKeyDown("a") || Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            i--;
            if (i < 0) i = allWeapons.Length - 1;
            weapon = allWeapons[i];
            gunController.changeWeapon();
        }
        if (Input.GetKeyDown("e") || Input.GetAxis("Mouse ScrollWheel") < 0f)
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
        refreshscreen();
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

    public int GetMunition()
    {
        if(weapon.ammoType == WeaponStats.AmmoType.shotgun)
        {
            return ShotgunAmmo;
        }
        else if(weapon.ammoType == WeaponStats.AmmoType.gun)
        {
            return GunAmmo;
        }
        else
        {
            return 0;
        }
    }

    public void AddMunition(int value)
    {
        int ammoRest = value - (weapon.maxMunitions - weapon.munitions);
        weapon.munitions = weapon.maxMunitions;

        if (weapon.ammoType == WeaponStats.AmmoType.shotgun)
        {
            ShotgunAmmo += ammoRest;
        }
        else if (weapon.ammoType == WeaponStats.AmmoType.gun)
        {
           GunAmmo += ammoRest;
        }

        if (ShotgunAmmo >= ShotgunAmmoMax)
        {
            ShotgunAmmo = ShotgunAmmoMax;
        }

        if (GunAmmo >= GunAmmoMax)
        {
            GunAmmo = GunAmmoMax;
        }
        refreshscreen();
    }

    public void AddGunAmmo(int value)
    {
        GunAmmo += value;
        if (GunAmmo >= GunAmmoMax)
        {
            GunAmmo = GunAmmoMax;
        }
        refreshscreen();
    }

    public void AddShotgunAmmo(int value)
    {
        ShotgunAmmo += value;
        if (ShotgunAmmo >= ShotgunAmmoMax)
        {
            ShotgunAmmo = ShotgunAmmoMax;
        }
        refreshscreen();
    }

    public void refreshscreen() {
        shotgunAmmoText.text = ShotgunAmmo + "";
        gunAmmoText.text = GunAmmo + "";
        textVie.text = (health * 100) / maxHealth + "%";
        textArmor.text = (armor * 100) / maxArmor + "%";
        gunAmmoMaxText.text = GunAmmoMax + "";
        shotgunAmmoMaxText.text = ShotgunAmmoMax + "";
    }
}