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

    void Start()
    {
        fpsCam = GetComponentInChildren<Camera>();
        inventory = GetComponent<PlayerInventory>();

        weapon = inventory.GetWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadAmmo();
        }

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && weapon.munitions == 0)
        {
            Debug.Log("Pas de munitions");
        }

        // V�rifie si le joueur a press� le bouton pour faire feu (bouton gauche de la souris)
        // Time.time > nextFire : v�rifie si suffisament de temps s'est �coul� pour pouvoir tirer � nouveau
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && weapon.munitions > 0)
        {
            //Nouveau tir
            weapon.munitions -= 1;

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
                Debug.Log("Ennemi Touch� !");
                // V�rifie si la cible a un RigidBody attach�
                if (hit.rigidbody != null)
                {
                    //S'assure que la cible touch�e a un composant ReceiveAction
                    if (hit.collider.gameObject.GetComponent<ReceiveAction>() != null)
                    {
                        //Envoie les dommages � la cible
                        hit.collider.gameObject.GetComponent<ReceiveAction>().GetDamage(weapon.wpnDmg);
                    }
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
            Debug.Log("Munitions remplies !");
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
    }
}
