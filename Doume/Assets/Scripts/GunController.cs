using UnityEngine;

public class GunController : MonoBehaviour
{
    //La caméra
    private Camera fpsCam;

    //Float : mémorise le temps du prochain tir possible
    private float nextFire;

    //Détermine sur quel Layer on peut tirer
    public LayerMask layerMask;

    private PlayerInventory inventory;
    [SerializeField]
    private WeaponStats weapon;

    public string enemieDamageFunction;

    public UnityEngine.UI.Image weaponImage;
    public UnityEngine.UI.Text ammoText;

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
        if (Input.GetButton("Fire1") && Time.time > nextFire && weapon.munitions > 0) {

            weapon.munitions--;

            //Met à jour le temps pour le prochain tir
            //Time.time = Temps écoulé depuis le lancement du jeu
            //temps du prochain tir = temps total écoulé + temps qu'il faut attendre
            nextFire = Time.time + weapon.fireRate;

            //On va lancer un rayon invisible qui simulera les balles du gun

            //Crée un vecteur au centre de la vue de la caméra
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            //RaycastHit : permet de savoir ce que le rayon a touché
            RaycastHit hit;


            // Vérifie si le raycast a touché quelque chose
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weapon.wpnRange, layerMask))
            {
                Debug.Log("Ennemi Touché !");
                // Vérifie si la cible a un RigidBody attaché
                if (hit.rigidbody != null)
                {
                    //S'assure que la cible touchée a un composant ReceiveAction
                    hit.collider.gameObject.SendMessage(enemieDamageFunction, weapon.wpnDmg);
                }
            }
        }
    }

    private void ReloadAmmo()
    {
        if (inventory.GetWeaponReloaderLeft() > 0 && weapon.munitions != weapon.maxMunitions)
        {
            weapon.munitions = weapon.maxMunitions;
            inventory.AddReloader(-1);
            Debug.Log("munitions remplies !");
        }
        else
        {
            Debug.Log("Echec du rechargement de munitions");
        }
    }

    public void changeWeapon()
    {
        weapon = inventory.GetWeapon();
        Debug.Log("Changement d'arme pour : " + weapon.name);
        weaponImage.sprite = weapon.Image;
    }
}