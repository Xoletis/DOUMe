using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Animator Animator;
    public AudioSource source;

    //La caméra
    private Camera fpsCam;
    private AudioManager audioManager;

    //Float : mémorise le temps du prochain tir possible
    private float nextFire;

    //Détermine sur quel Layer on peut tirer
    public LayerMask mask;

    private PlayerInventory inventory;
    [SerializeField]
    private WeaponStats weapon;

    public string enemieDamageFunction;

    public UnityEngine.UI.Image weaponImage;
    public UnityEngine.UI.Text ammoText;

    bool canFire = true;

    void Start()
    {
        fpsCam = GetComponentInChildren<Camera>();
        inventory = GetComponent<PlayerInventory>();

        weapon = inventory.GetWeapon();
        weaponImage.sprite = weapon.Image;
        audioManager = FindObjectOfType<AudioManager>();
        Animator.SetInteger("Ammo", weapon.munitions);
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * weapon.wpnRange, Color.white);

        ammoText.text = weapon.munitions + "";

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadAmmo();
        }

        if (Input.GetButton("Fire1") && Time.time > nextFire && weapon.munitions == 0)
        {
            Debug.Log("Pas de munitions");

        }

        // Vérifie si le joueur a pressé le bouton pour faire feu (bouton gauche de la souris)
        // Time.time > nextFire : vérifie si suffisament de temps s'est écoulé pour pouvoir tirer à nouveau
        if (Input.GetButton("Fire1") && Time.time > nextFire && weapon.munitions > 0 && canFire)
        {
            weapon.munitions--;
            attack(weapon.wpnDmg * GetComponent<PlayerInventory>().stat.multiply);
        }
        else if(Input.GetButton("Fire1") && Time.time > nextFire && weapon.nom == "Sword")
        {
            attack(1.0f * GetComponent<PlayerInventory>().stat.multiply);
        }
    }

    //Recharge de l'arme actuelle
    private void ReloadAmmo()
    {
        if (inventory.GetMunition() > 0 && weapon.munitions != weapon.maxMunitions)
        {
            StartCoroutine(reloadTime());
        }
        else
        {
            Debug.Log("Echec du rechargement de munitions");
        }
    }

    //Changement d'arme
    public void changeWeapon()
    {
        Animator.SetTrigger("switchweapon");
        weapon = inventory.GetWeapon();
        weaponImage.sprite = weapon.Image;
    }

    //temps de rechargement
    IEnumerator reloadTime()
    {
        source.clip = weapon.reloadSound;
        source.Play();
        canFire = false;
        weaponImage.enabled = false;
        yield return new WaitForSeconds(weapon.reaoldTime);
        int munitionARajouter = weapon.maxMunitions - weapon.munitions;
        if (inventory.GetMunition() < weapon.maxMunitions)
        {
            weapon.munitions = inventory.GetMunition();
            inventory.AddMunition(-inventory.GetMunition());
        }
        else
        {
            weapon.munitions = weapon.maxMunitions;
            inventory.AddMunition(-munitionARajouter);
        }
        Debug.Log("munitions remplies !");
        canFire = true;
        weaponImage.enabled = true;
        Animator.SetInteger("Ammo", weapon.munitions);
    }

    public void attack(float dammage)
    {
        source.clip = weapon.firingSound;
        source.Play();
        Animator.SetTrigger("pan");

        //Met à jour le temps pour le prochain tir
        //Time.time = Temps écoulé depuis le lancement du jeu
        //temps du prochain tir = temps total écoulé + temps qu'il faut attendre
        nextFire = Time.time + weapon.fireRate;

        //On va lancer un rayon invisible qui simulera les balles du gun

        //Crée un vecteur au centre de la vue de la caméra
        Vector3 rayOrigin = fpsCam.transform.position;

        //RaycastHit : permet de savoir ce que le rayon a touché
        RaycastHit hit;

        if (GetComponent<PlayerInventory>().Invincible)
        {
            dammage = 10000000;
        }

        // Vérifie si le raycast a touché quelque chose
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, weapon.wpnRange, mask))
        {
            Debug.Log(hit.collider.name);
            if (hit.transform.tag == "ExplosiveBarrel")
            {
                Debug.Log("Barrel Touché !");
                hit.transform.GetComponent<ExplosiveBarrel>().Explode();
            }
            else if (hit.transform.tag == "obama")
            {

                hit.transform.GetComponent<obama>().surprise(hit.transform.tag);

            }
            else
            {
                hit.collider.gameObject.SendMessage(enemieDamageFunction, dammage);
            }
        }
        Animator.SetInteger("Ammo", weapon.munitions);
    }
}