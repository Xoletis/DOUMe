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
    public float health;
    public GameObject blood;
    public AudioSource source;

    public bool isInvicible = false;

    [SerializeField]
    public item[] DropList;

    public DestroyRandomDoor door;

    public Transform spawnBullet;

    public Animator animator;

    void Start()
    {
        //assignation des différentes variables privées
        Player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;
        health = data.health;
        attackCouldown = 0;
        if(door != null)
            door.AddEnnemy();
        animator = GetComponent<Animator>();
        StartCoroutine(sound());
    }

    void Update()
    {
        if (!isInvicible)
        {
            //verifie si le joueur est à portée de chasse
            if (PlayerDetected())
            {
                //verifie si le joueur est à portée d'attaque
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

                    //stop le déplacement
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
                //stop le déplacement
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

    //dessine les sphéres de debug
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
            Debug.Log("Touché");
            health -= damage;
            //on instantie les particules de sang
            SetBlood();

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
        //on enléve un ennemei de la sale
        if(door != null)
            door.DestroyEnnemy();
        //on instantie les particules de sang
        SetBlood();
        //Drop du loot alléatoire
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
        source.clip = data.attackSound;
        source.Play();
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 dir = (playerPos - transform.position).normalized;
        Vector3 vector = new Vector3(0, 0, 2f) + dir;

        GameObject bullet = Instantiate(data.bullet, spawnBullet.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().damage = data.damage;
        bullet.GetComponent<Bullet>().playerDamageFonctionName = playerDamageFonctionName;
    }

    public void AttackContact()
    {
        source.clip = data.attackSound;
        source.Play();
        Player.SendMessage(playerDamageFonctionName, data.damage);
    }

    public void Explose(int damage)
    {
        if (PlayerDetected())
        {
            Player.SendMessage(playerDamageFonctionName, damage);
        }
    }

    public void SetBlood()
    {
        Instantiate(blood, transform.position, Quaternion.identity);
    }

    public void Flash()
    {
        Player.GetComponent<PlayerInventory>().WhitFlash();
    }

    public void Ralentisement()
    {
        Player.GetComponent<PlayerInventory>().Ralatie();
    }

    IEnumerator sound()
    {
        int m = Random.Range(0, 4);
        yield return new WaitForSeconds(m);
        while (true){
            yield return new WaitForSeconds(1);
            int n = Random.Range(0, 5);
            if (n == 0)
            {
                if (source != null)
                {
                    source.clip = data.natuarlSound;
                    source.Play();
                }
            }
        }
    }
}
