using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BossIA))]
public class BossCapacity : MonoBehaviour
{

    public bool isActive;
    protected Animator animator;
    public string Annime;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void Attack()
    {
        
    }

    public void Active()
    {
        isActive = !isActive;
        if (isActive)
        {
            Attack();
        }
        else
        {
            GetComponent<BossIA>().StartCouldown();
        }
    }
}
