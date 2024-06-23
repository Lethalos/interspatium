using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyHealth : MonoBehaviour
{
    public int Enemyhealth = 50;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        var movementScript = GetComponent<RangedEnemy>();
        if (Enemyhealth <= 0)
        {
            animator.SetBool("isDied", true);
            movementScript.enabled = false;
        }
    }
}
