using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //La cam�ra
    private Camera fpsCam;

    //Float : m�morise le temps du prochain tir possible
    private float nextFire;

    //D�termine sur quel Layer on peut tirer
    public LayerMask layerMask;

    private PlayerInventory inventory;
    [SerializeField]
    private WeaponStats weapon;

    public string enemieDamageFunction;

    public UnityEngine.UI.Image weaponImage;
    public UnityEngine.UI.Text ammoText;

    bool canFire = true;

    private AudioManager audioManager;

    void Start()
    {
        fpsCam = GetComponentInChildren<Camera>();
        inventory = GetComponent<PlayerInventory>();

        audioManager = FindObjectOfType<AudioManager>();

        weapon = inventory.GetWeapon();
        weaponImage.sprite = weapon.Image;
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = weapon.munitions + "";

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadAmmo();
        }

        
        if (Input.GetButton("Fire1") && Time.time > nextFire && weapon.munitions == 0)
        {
            Debug.Log("Pas de munitions");

        }

        // V�rifie si le joueur a press� le bouton pour faire feu (bouton gauche de la souris)
        // Time.time > nextFire : v�rifie si suffisament de temps s'est �coul� pour pouvoir tirer � nouveau
        if (Input.GetButton("Fire1") && Time.time > nextFire && weapon.munitions > 0 && canFire) {
           
            audioManager.Play(weapon.firingSound);
            weapon.munitions--;

            //Met � jour le temps pour le prochain tir
            //Time.time = Temps �coul� depuis le lancement du jeu
            //temps du prochain tir = temps total �coul� + temps qu'il faut attendre
            nextFire = Time.time + weapon.fireRate;

            //On va lancer un rayon invisible qui simulera les balles du gun

            //Cr�e un vecteur au centre de la vue de la cam�ra
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            //RaycastHit : permet de savoir ce que le rayon a touch�
            RaycastHit hit;


            // V�rifie si le raycast a touch� quelque chose
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weapon.wpnRange, layerMask))
            {
                // V�rifie si la cible a un RigidBody attach�
                if (hit.rigidbody != null)
                {
                    //S'assure que la cible touch�e a un composant ReceiveAction
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
        audioManager.Play(weapon.reloadSound);
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