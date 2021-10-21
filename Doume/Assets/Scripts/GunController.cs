using UnityEngine;

public class GunController : MonoBehaviour
{
    //Dommage que le Gun inflige
    public int gunDamage = 1;

    //Port�e du tir
    public float weaponRange = 200f;

    //La cam�ra
    private Camera fpsCam;

    //Temps entre chaque tir (en secondes) 
    public float fireRate = 0.25f;

    //Float : m�morise le temps du prochain tir possible
    private float nextFire;

    //D�termine sur quel Layer on peut tirer
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

        // V�rifie si le joueur a press� le bouton pour faire feu (bouton gauche de la souris)
        // Time.time > nextFire : v�rifie si suffisament de temps s'est �coul� pour pouvoir tirer � nouveau
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && ammo > 0)
        {
            //Nouveau tir
            ammo -= 1;

            //Met � jour le temps pour le prochain tir
            //Time.time = Temps �coul� depuis le lancement du jeu
            //temps du prochain tir = temps total �coul� + temps qu'il faut attendre
            nextFire = Time.time + fireRate;

            //On va lancer un rayon invisible qui simulera les balles du gun

            //Cr�e un vecteur au centre de la vue de la cam�ra
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            //RaycastHit : permet de savoir ce que le rayon a touch�
            RaycastHit hit;


            // V�rifie si le raycast a touch� quelque chose
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange, layerMask))
            {
                Debug.Log("Ennemi Touch� !");
                // V�rifie si la cible a un RigidBody attach�
                if (hit.rigidbody != null)
                {
                    //S'assure que la cible touch�e a un composant ReceiveAction
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