using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShot : MonoBehaviour
{
    public void TakeDamage(float damage)
    {
        Debug.Log("Touch� Head");
        GetComponentInParent<EnnemieAi>().TakeDamage(damage * 2);
    }
}
