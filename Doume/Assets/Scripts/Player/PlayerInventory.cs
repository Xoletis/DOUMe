using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerInventory : MonoBehaviour
{

    public WeaponStats[] allWeapons;
    [SerializeField]
    private WeaponStats weapon;
    private int i = 0;

    private GunController gunController;

    public Stat stat;

    public Text textVie;
    public Text textArmor;

    public Text shotgunAmmoText;
    public Text gunAmmoText;
    public Text shotgunAmmoMaxText;
    public Text gunAmmoMaxText;
    public Text energyAmmoMaxText;
    public Text energyAmmoText;

    public GameObject flash;
    public GameObject Slowness;

    public Volume v;
    private Vignette vg;

    private void Awake()
    {
        for (int i = 0; i < allWeapons.Length; i++)
        {
            allWeapons[i].munitions = allWeapons[i].maxMunitions;
        }

        PlayerPrefs.SetInt("Score", 0);

        weapon = allWeapons[0];
        stat.health = stat.maxHealth;
        stat.GunAmmo = stat.GunAmmoMax;
        stat.ShotgunAmmo = stat.ShotgunAmmoMax;
        stat.EnergyAmmo = stat.EnergyAmmoMax;
        refreshscreen();
        v.profile.TryGet(out vg);
        LeftHealth();
    }

    // Start is called before the first frame update
    void Start()
    {
        stat.armor = 0;

        gunController = gameObject.GetComponent<GunController>();
        refreshscreen();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            SwitchWeapon();
        }
    }

    void SwitchWeapon()
    {
        i--;
        if (i < 0) i = allWeapons.Length - 1;
        weapon = allWeapons[i];
        gunController.changeWeapon();
    }

    // Le joueur perd des points de vie / d'armure
    public void HurtPlayer(int damage)
    {
        if (stat.armor > damage)
            stat.armor -= damage;
        else if (stat.armor < damage)
        {
            stat.health -= (damage - stat.armor);
            stat.armor = 0;
        }
        LeftHealth();
        refreshscreen();
    }

    // Le joueur est-il mort ?
    public bool IsDead()
    {
        return stat.health <= 0;
    }

    //on récupére l'arme actuelle
    public WeaponStats GetWeapon()
    {
        return weapon;
    }

    //On récupere le nombre de munition actuelle
    public int GetMunition()
    {
        if(weapon.ammoType == WeaponStats.AmmoType.shotgun)
        {
            return stat.ShotgunAmmo;
        }
        else if(weapon.ammoType == WeaponStats.AmmoType.gun)
        {
            return stat.GunAmmo;
        }
        else if(weapon.ammoType == WeaponStats.AmmoType.energy)
        {
            return stat.EnergyAmmo;
        }
        else
        {
            return 0;
        }
    }

    // ajout de n'importe quelle munition
    public void AddMunition(int value)
    {
        int ammoRest = value - (weapon.maxMunitions - weapon.munitions);
        weapon.munitions = weapon.maxMunitions;

        if (weapon.ammoType == WeaponStats.AmmoType.shotgun)
        {
            stat.ShotgunAmmo += ammoRest;
        }
        else if (weapon.ammoType == WeaponStats.AmmoType.gun)
        {
            stat.GunAmmo += ammoRest;
        }else if(weapon.ammoType == WeaponStats.AmmoType.energy)
        {
            stat.EnergyAmmo += ammoRest;
        }

        if (stat.ShotgunAmmo >= stat.ShotgunAmmoMax)
        {
            stat.ShotgunAmmo = stat.ShotgunAmmoMax;
        }

        if (stat.GunAmmo >= stat.GunAmmoMax)
        {
            stat.GunAmmo = stat.GunAmmoMax;
        }
        
        if(stat.EnergyAmmo >= stat.EnergyAmmoMax)
        {
            stat.EnergyAmmo = stat.EnergyAmmoMax;
        }
        refreshscreen();
    }

    //Ajout de balle de pistolet
    public void AddGunAmmo(int value)
    {
        stat.GunAmmo += value;
        if (stat.GunAmmo >= stat.GunAmmoMax)
        {
            stat.GunAmmo = stat.GunAmmoMax;
        }
        refreshscreen();
    }

    //Ajout de balle de Shotgun
    public void AddShotgunAmmo(int value)
    {
        stat.ShotgunAmmo += value;
        if (stat.ShotgunAmmo >= stat.ShotgunAmmoMax)
        {
            stat.ShotgunAmmo = stat.ShotgunAmmoMax;
        }
        refreshscreen();
    }

    public void AddEnergyAmmo(int value)
    {
        stat.EnergyAmmo += value;
        if (stat.EnergyAmmo >= stat.EnergyAmmoMax)
        {
            stat.EnergyAmmo = stat.EnergyAmmoMax;
        }
        refreshscreen();
    }

    //Ajout d'armur
    public void AddArmor(int value)
    {
        stat.armor += value;
        if(stat.armor > stat.maxArmor)
        {
            stat.armor = stat.maxArmor;
        }

        refreshscreen();
    }

    //Ajout de vie
    public void AddHealth(int value)
    {
        stat.health += value;
        if (stat.health > stat.maxHealth)
        {
            stat.health = stat.maxHealth;
        }
        LeftHealth();
        refreshscreen();
    }

    public void TimeHealing(int Value)
    {
        StartCoroutine(healing(Value));
    }

    public void AddMaxHealth(int value)
    {
        stat.maxHealth += value;
        stat.health += value;
        refreshscreen();
    }

    //Actualise le UI
    public void refreshscreen() {
        shotgunAmmoText.text = stat.ShotgunAmmo + "";
        gunAmmoText.text = stat.GunAmmo + "";
        textVie.text = (stat.health * 100) / stat.maxHealth + "%";
        textArmor.text = (stat.armor * 100) / stat.maxArmor + "%";
        gunAmmoMaxText.text = stat.GunAmmoMax + "";
        shotgunAmmoMaxText.text = stat.ShotgunAmmoMax + "";
        energyAmmoText.text = stat.EnergyAmmo + "";
        energyAmmoMaxText.text = stat.EnergyAmmoMax + "";
    }

    /// <summary>
    /// max -> 100
    /// health -> ?
    /// </summary>
    public void LeftHealth()
    {
        if ((stat.health * 100) / stat.maxHealth <= 100)
        {
            vg.intensity.value = 0.5f;
        }
        if ((stat.health * 100) / stat.maxHealth <= 80)
        {
            vg.intensity.value = 0.6f;
        }
        if ((stat.health * 100) / stat.maxHealth <= 60)
        {
            vg.intensity.value = 0.7f;
        }
        if ((stat.health * 100) / stat.maxHealth <= 40)
        {
            vg.intensity.value = 0.8f;
        }
        if ((stat.health * 100) / stat.maxHealth <= 20)
        {
            vg.intensity.value = 0.9f;
        }
    }

    public void WhitFlash()
    {
        StartCoroutine(Flash());
    }

    public void Ralatie()
    {
        StartCoroutine(Raletisement());
    }

    IEnumerator Flash()
    {
        flash.SetActive(true);
        yield return new WaitForSeconds(2);
        flash.SetActive(false);
    }

    IEnumerator Raletisement()
    {
        GetComponent<PlayerFPS>().walkingSpeed /= 2; 
        GetComponent<PlayerFPS>().runningSpeed /= 2;
        Slowness.SetActive(true);
        yield return new WaitForSeconds(5);
        Slowness.SetActive(false);
        GetComponent<PlayerFPS>().walkingSpeed *= 2;
        GetComponent<PlayerFPS>().runningSpeed *= 2;
    }

    IEnumerator healing(int heal)
    {
        while(heal > 0)
        {
            AddHealth(1);
            heal--;
            yield return new WaitForSeconds(0.5f);
        }
    }

}

//Stat du joueur
[System.Serializable]
public struct Stat
{
    public int health;
    public int armor;
    public int maxHealth;
    public int maxArmor;
    public int ShotgunAmmo;
    public int GunAmmo;
    public int ShotgunAmmoMax;
    public int GunAmmoMax;
    public int EnergyAmmo;
    public int EnergyAmmoMax;
}