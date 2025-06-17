using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float detectionRadius = 5f;
    public string playerTag = "Player";
    public float damageCooldown = 1f; // time between each hit

    private Transform player;
    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private Rigidbody2D rb;
    public Transform[] patrolPoints;

    private NavMeshAgent agent;
    public bool canMove = true;
    private bool hasTaggedPlayer = false;
    private float damageTimer = 0f;

    private PlayerMovement playerMovement;
    private HealthManager healthManager;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        healthManager = FindFirstObjectByType<HealthManager>();

        patrolPoints = GameObject.Find("Patrol Points").GetComponentsInChildren<Transform>();
        patrolPoints = System.Array.FindAll(patrolPoints, t => t.name.StartsWith("PatrolPoint"));

        if (patrolPoints.Length == 0)
        {
            Debug.LogWarning("No patrol points found as children of enemy!");
        }
        else
        {
            System.Array.Sort(patrolPoints, (a, b) => a.name.CompareTo(b.name));
        }
    }

    void Update()
    {
        if (!canMove)
        {
            agent.isStopped = true;
            damageTimer += Time.deltaTime;
            TryDrainHealth();
            return;
        }

        if (player == null || patrolPoints.Length == 0) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        isChasing = distanceToPlayer <= detectionRadius;

        if (isChasing)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        Vector2 targetPoint = patrolPoints[currentPatrolIndex].position;

        if (Vector2.Distance(transform.position, targetPoint) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        agent.SetDestination(targetPoint);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && !hasTaggedPlayer)
        {
            hasTaggedPlayer = true;
            canMove = false;

            if (playerMovement != null)
                playerMovement.canMove = false;

            damageTimer = damageCooldown; // so the first hit happens instantly
        }
    }

    private void TryDrainHealth()
    {
        if (hasTaggedPlayer && damageTimer >= damageCooldown && healthManager != null)
        {
            healthManager.TakeDamage();
            damageTimer = 0f;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
