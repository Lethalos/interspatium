using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int enemyhealth = 50;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var movementScript = GetComponent<MeleeEnemy>();
        if (enemyhealth <= 0)
        {
            animator.SetBool("isDied", true);
            movementScript.enabled = false;
            StartCoroutine(DestroyEnemy());
        }
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);

        if (GetComponent<MeleeEnemy>() != null)
        {
            FindObjectOfType<EnemyPoolHandler>().ReturnToMeleeEnemyPool(this.GetComponent<MeleeEnemy>()); // Adjust this as necessary
        }
        else if (GetComponent<RangedEnemy>() != null)
        {
            FindObjectOfType<EnemyPoolHandler>().ReturnToRangedEnemyPool(this.GetComponent<RangedEnemy>()); // Adjust this as necessary
        }
    }
}