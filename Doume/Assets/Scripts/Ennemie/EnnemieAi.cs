using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public struct item{
    public GameObject DropObject;
    public int chance;
}

[RequireComponent(typeof(NavMeshAgent))]
public class EnnemieAi : MonoBehaviour
{
    [Tooltip("stats de l'ennemie")]
    public EnnemieData data;
    //r�f�rence au joueur
    private GameObject Player;
    //composent NavMeshAgent pour d�placer l'ennemie
    private NavMeshAgent agent;
    [Tooltip("fonction � appeler pour fair des d�gats au joueur")]
    public string playerDamageFonctionName;
    //couldown de l'attaque
    [SerializeField]
    float attackCouldown;
    [SerializeField]
    public float health;
    public GameObject blood;

    public bool isInvicible = false;

    [SerializeField]
    public item[] DropList;

    public DestroyRandomDoor door;

    public Transform spawnBullet;

    public Animator animator;

    void Start()
    {
        //assignation des diff�rentes variables priv�es
        Player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;
        health = data.health;
        attackCouldown = data.attackCouldown;
        if(door != null)
            door.AddEnnemy();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isInvicible)
        {
            //verifie si le joueur est � port�e de chasse
            if (PlayerDetected())
            {
                //verifie si le joueur est � port�e d'attaque
                if (Vector3.Distance(transform.position, Player.transform.position) <= data.attackRange)
                {
                    //attaque si le couldown est fini
                    if (attackCouldown <= 0.0f)
                    {
                        //Si a distance on tire sinon on attack
                        animator.SetTrigger("Attaque");
                        attackCouldown = data.attackCouldown;
                    }
                    else
                    {
                        //on baisse le couldown
                        attackCouldown -= Time.deltaTime;
                    }

                    //stop le d�placement
                    agent.isStopped = true;
                }
                else
                {
                    //lance le d�placement
                    agent.isStopped = false;
                    agent.destination = Player.transform.position;
                }
            }
            else
            {
                //stop le d�placement
                agent.isStopped = true;
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }

    //fonction qui vois si l'ennemie voit le joeur
    bool PlayerDetected()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, data.viewArea);

        foreach (Collider col in objects)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    //dessine les sph�res de debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.viewArea);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, data.attackRange);
    }


    //prendre des dommages
    public void TakeDamage(float damage)
    {
        if (!isInvicible)
        {
            Debug.Log("Touch�");
            health -= damage;
            //on instantie les particules de sang
            Instantiate(blood, transform.position, Quaternion.identity);

            //mort
            if (health <= 0)
            {
                if (animator != null)
                    animator.SetTrigger("Death");
                else
                    Death();
            }
        }
    }

    public void Death()
    {
        //on enl�ve un ennemei de la sale
        if(door != null)
            door.DestroyEnnemy();
        //on instantie les particules de sang
        Instantiate(blood, transform.position, Quaternion.identity);
        //Drop du loot all�atoire
        if (Random.Range(0, 100) < data.droopRate)
        {
            List<GameObject> objetToDrop = new List<GameObject>();
            for (int i = 0; i < DropList.Length; i++)
            {
                for (int j = 0; j < DropList[i].chance; j++)
                {
                    objetToDrop.Add(DropList[i].DropObject);
                }
            }

            int dropObject = Random.Range(0, objetToDrop.Count);
            Instantiate(objetToDrop[dropObject], transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    public void invincible()
    {
        isInvicible = true;
    }

    public void RangedAttack()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 dir = (playerPos - transform.position).normalized;
        Vector3 vector = new Vector3(0, 0, 2f) + dir;

        GameObject bullet = Instantiate(data.bullet, spawnBullet.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().damage = data.damage;
        bullet.GetComponent<Bullet>().playerDamageFonctionName = playerDamageFonctionName;
    }

    public void AttackContact()
    {
        Player.SendMessage(playerDamageFonctionName, data.damage);
    }

    public void Explose()
    {
        if (PlayerDetected())
        {
            Player.SendMessage(playerDamageFonctionName, data.damage * 2);
        }
    }
}
