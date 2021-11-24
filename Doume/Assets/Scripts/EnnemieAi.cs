using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnnemieAi : MonoBehaviour
{

    [Tooltip("stats de l'ennemie")]
    public EnnemieData data;
    //référence au joueur
    private GameObject Player;
    //composent NavMeshAgent pour déplacer l'ennemie
    private NavMeshAgent agent;
    [Tooltip("fonction à appeler pour fair des dégats au joueur")]
    public string playerDamageFonctionName;
    //couldown de l'attaque
    [SerializeField]
    float attackCouldown;
    [SerializeField]
    private float health;

    public GameObject[] DropList;

    public DestroyRandomDoor door;

    void Start()
    {
        //assignation des différentes variables privées
        Player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;
        health = data.health;
        attackCouldown = data.attackCouldown;
        door.AddEnnemy();
    }

    void Update()
    {
        //verifie si le joueur est à portée de chasse
        if (PlayerDetected())
        {
            //verifie si le joueur est à portée d'attaque
            if (Vector3.Distance(transform.position, Player.transform.position) <= data.attackRange)
            {
                //attaque si le couldown est fini
                if(attackCouldown <= 0.0f)
                {
                    if (data.isRangeEnnemie)
                    {
                        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                        Vector3 dir = (playerPos - transform.position).normalized;
                        Vector3 vector = new Vector3(0, 0, 2f) + dir;
                        
                        GameObject bullet = Instantiate(data.bullet, transform.position, Quaternion.identity);
                        bullet.GetComponent<Bullet>().damage = data.damage;
                        bullet.GetComponent<Bullet>().playerDamageFonctionName = playerDamageFonctionName;
                        
                    }
                    else
                    {
                        Player.SendMessage(playerDamageFonctionName, data.damage);
                    }
                    attackCouldown = data.attackCouldown;
                }
                else
                {
                    attackCouldown -= Time.deltaTime;
                }
               
                agent.isStopped = true;
            }
            else
            {
                //lance le déplacement
                agent.isStopped = false;
                agent.destination = Player.transform.position;
            }
        }
        else
        {
            //srop le déplacement
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

    //dessine les sphére de débug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.viewArea);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, data.attackRange);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            door.DestroyEnnemy();
            if(Random.Range(0,100) < data.droopRate)
            {
                int objectIndexToDrop = Random.Range(0, DropList.Length);
                Instantiate(DropList[objectIndexToDrop], transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
