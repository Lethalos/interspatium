using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpmareBehaviour : MonoBehaviour
{
    [SerializeField] private Transform player; // Player to follow
    private float visibilityRange = 20f; // Range at which the enemy becomes visible
    private float attackRange = 10f; // Range at which the enemy starts attacking
    private float attackInterval = 1.5f; // Time between attacks

    private Animator animator;
    private string currentAnimation = "";
    private bool isGrounded = false;
    private bool canAttack = true; // Variable to track if the enemy can attack

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= visibilityRange)
        {
            if (!isGrounded)
            {
                ChangeAnimation("Ground");
                StartCoroutine(IsGrounded(true));
                return;
            }

            if (distanceToPlayer <= attackRange)
            {
                if (canAttack)
                {
                    StartCoroutine(Attack());
                }
            }
            else
            {
                ChangeAnimation("Idle");
            }
        }
    }

    public void KillEnemy()
    {
        this.enabled = false;

        // Disable the collider
        GetComponent<BoxCollider>().enabled = false;

        // Disable the GameObject
        ChangeAnimation("Death");

        gameObject.tag = "Untagged";
    }

    private IEnumerator IsGrounded(bool isGrounded)
    {
        yield return new WaitForSeconds(1f);
        this.isGrounded = isGrounded;
    }

    private IEnumerator Attack()
    {
        canAttack = false; // Set canAttack to false to start the cooldown

        int randomAttack = Random.Range(0, 2);
        if (randomAttack == 0)
        {
            ChangeAnimation("Attack1");
        }
        else
        {
            ChangeAnimation("Attack2");
        }

        yield return new WaitForSeconds(attackInterval); // Wait for the attack interval
        canAttack = true; // Set canAttack back to true to allow another attack
    }

    public void ChangeAnimation(string animation, float crossFade = 0.2f)
    {
        if (currentAnimation == animation) return;

        currentAnimation = animation;
        animator.CrossFade(animation, crossFade);
    }
}
