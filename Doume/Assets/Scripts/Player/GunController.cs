using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //La caméra
    private Camera fpsCam;

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

            //Met à jour le temps pour le prochain tir
            //Time.time = Temps écoulé depuis le lancement du jeu
            //temps du prochain tir = temps total écoulé + temps qu'il faut attendre
            nextFire = Time.time + weapon.fireRate;

            //On va lancer un rayon invisible qui simulera les balles du gun

            //Crée un vecteur au centre de la vue de la caméra
            Vector3 rayOrigin = fpsCam.transform.position;

            //RaycastHit : permet de savoir ce que le rayon a touché
            RaycastHit hit;

            // Vérifie si le raycast a touché quelque chose
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, weapon.wpnRange, mask))
            {
                Debug.Log(hit.collider.name);
                if (hit.transform.tag == "ExplosiveBarrel")
                {
                    Debug.Log("Barrel Touché !");
                    hit.transform.GetComponent<ExplosiveBarrel>().Explode();
                }
                else
                {
                    hit.collider.gameObject.SendMessage(enemieDamageFunction, weapon.wpnDmg);
                }
            }
        }
    }

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

    public void changeWeapon()
    {
        weapon = inventory.GetWeapon();
        weaponImage.sprite = weapon.Image;
    }

    IEnumerator reloadTime()
    {
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
    }
}