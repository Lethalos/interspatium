using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 3.5f;
    public float escapeDistance = 10f;
    public float enemyDetectDistance = 10f;
    public Animator animator;
    public GameObject escapeVFX;
    public float escapeDuration = 2f; // Duration to wait before making the NPC invisible
    public float descendSpeed = 2f;   // Speed at which the NPC descends
    public float rotationSpeed = 90f; // Rotation speed for y-axis rotation during descent


    private NavMeshAgent agent;
    private bool hasEscaped = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (escapeVFX != null)
        {
            escapeVFX.SetActive(false); // Ensure the VFX is initially disabled
        }
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (player == null || hasEscaped) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Transform nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, nearestEnemy.position);
            if (distanceToEnemy <= enemyDetectDistance)
            {
                StartCoroutine(EscapeFromEnemy(nearestEnemy));
                return;
            }
        }

        if (distanceToPlayer > agent.stoppingDistance)
        {
            // Follow the player
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("isRunning", true);
            animator.SetBool("isEscaping", false);
        }
        else
        {
            // Idle
            StopAgent();
            animator.SetBool("isRunning", false);
            animator.SetBool("isEscaping", false);
        }
    }

    Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }

    IEnumerator EscapeFromEnemy(Transform enemy)
    {
        hasEscaped = true;

        // Trigger escape animation
        StopAgent();
        animator.SetBool("isRunning", false);
        animator.SetBool("isEscaping", true);

        // Wait for the escape animation to start
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Escape"));

        // Trigger escape VFX
        if (escapeVFX != null)
        {
            escapeVFX.SetActive(true);
        }

        // Wait for the escape animation to finish
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Make NPC invisible and start descending
        StartCoroutine(DescendAndDisableNPC());
    }

    IEnumerator DescendAndDisableNPC()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition - new Vector3(0, 5f, 0); // Adjust the y-value to control how far the NPC descends

        while (elapsedTime < escapeDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, (elapsedTime / escapeDuration));
            // Rotate around y-axis
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        // Disable renderers to make the NPC invisible
        Renderer npcRenderer = GetComponent<Renderer>();
        if (npcRenderer != null)
        {
            npcRenderer.enabled = false;
        }
        // Disable renderers of all child objects except the VFX
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            if (r.gameObject != escapeVFX)
            {
                r.enabled = false;
            }
        }
    }

    void StopAgent()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }
}
