using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private float health;

    public DestroyRandomDoor door;

    private void Awake()
    {
       
    }

    void Start()
    {
        //assignation des diff�rentes variables priv�es
        Player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;
        health = data.health;
        attackCouldown = data.attackCouldown;
        door.AddEnnemy();
    }

    void Update()
    {
        //verifie si le joueur est � port�e de chasse
        if (PlayerDetected())
        {
            //verifie si le joueur est � port�e d'attaque
            if (Vector3.Distance(transform.position, Player.transform.position) <= data.attackRange)
            {
                //attaque si le couldown est fini
                if(attackCouldown <= 0.0f)
                {
                    Player.SendMessage(playerDamageFonctionName, data.damage);
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
                //lance le d�placement
                agent.isStopped = false;
                agent.destination = Player.transform.position;
            }
        }
        else
        {
            //srop le d�placement
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

    //dessine les sph�re de d�bug
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
        Debug.Log("Vous avvez donner " + damage + " d�gat � " + this.name);
        if(health <= 0)
        {
            Debug.Log(gameObject.name + " est mort");
            door.DestroyEnnemy();
            Destroy(gameObject);
        }
    }
}
