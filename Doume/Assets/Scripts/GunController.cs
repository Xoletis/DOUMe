using UnityEngine;

public class GunController : MonoBehaviour
{
    //Dommage que le Gun inflige
    public int gunDamage = 1;

    //Portée du tir
    public float weaponRange = 200f;

    //La caméra
    private Camera fpsCam;

    //Temps entre chaque tir (en secondes) 
    public float fireRate = 0.25f;

    //Float : mémorise le temps du prochain tir possible
    private float nextFire;

    //Détermine sur quel Layer on peut tirer
    public LayerMask layerMask;

    [SerializeField]
    private int ammo;
    private int maxAmmo;

    private PlayerInventory inventory;
    private WeaponStats weapon;

    void Start()
    {
        fpsCam = GetComponentInChildren<Camera>();
        inventory = GetComponent<PlayerInventory>();

        weapon = inventory.GetWeapon();
        gunDamage = weapon.wpnDmg;
        weaponRange = weapon.wpnRange;
        fireRate = weapon.fireRate;
        ammo = weapon.maxMunitions;
        maxAmmo = weapon.maxMunitions;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadAmmo();
        }

        // Vérifie si le joueur a pressé le bouton pour faire feu (bouton gauche de la souris)
        // Time.time > nextFire : vérifie si suffisament de temps s'est écoulé pour pouvoir tirer à nouveau
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && ammo > 0)
        {
            //Nouveau tir
            ammo -= 1;

            //Met à jour le temps pour le prochain tir
            //Time.time = Temps écoulé depuis le lancement du jeu
            //temps du prochain tir = temps total écoulé + temps qu'il faut attendre
            nextFire = Time.time + fireRate;

            //On va lancer un rayon invisible qui simulera les balles du gun

            //Crée un vecteur au centre de la vue de la caméra
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            //RaycastHit : permet de savoir ce que le rayon a touché
            RaycastHit hit;


            // Vérifie si le raycast a touché quelque chose
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange, layerMask))
            {
                Debug.Log("Ennemi Touché !");
                // Vérifie si la cible a un RigidBody attaché
                if (hit.rigidbody != null)
                {
                    //S'assure que la cible touchée a un composant ReceiveAction
                    hit.collider.gameObject.SendMessage("TakeDamage", weapon.wpnDmg);
                }
            }
        }
    }

    private void ReloadAmmo()
    {
        if (inventory.GetWeaponReloaderLeft() > 0)
        {
            ammo = maxAmmo;
            inventory.AddReloader(-1);
        }
    }
}