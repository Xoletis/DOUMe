using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{

    public GameObject Boss;
    public GameObject Lave;

    public void spawn()
    {
        if(PlayerPrefs.GetInt("Level") != 0 && PlayerPrefs.GetInt("Level") % 5 == 0)
        {
            GameObject _boss = Instantiate(Boss, transform.position, Quaternion.identity);
            _boss.GetComponent<BossIA>().lave = Lave;
            Lave.GetComponent<LaveQuiMonte>().boss = _boss;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spawn();
        }
    }
}
