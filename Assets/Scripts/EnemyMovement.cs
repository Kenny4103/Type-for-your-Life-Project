using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float patrolSpeed = 2f;          // Speed during patrol
    public float chaseSpeed = 3.5f;         // Speed when chasing player
    public float detectionRadius = 5f;      // Radius to detect player
    public Transform[] patrolPoints;        // Array of patrol points for looping
    public string playerTag = "Player";     // Tag for the player
    public LayerMask obstacleLayer;         // Layer for obstacles (not used for collision)

    private Transform player;               // Reference to player's transform
    private int currentPatrolIndex = 0;     // Current patrol point index
    private bool isChasing = false;         // Whether enemy is chasing player
    private Rigidbody2D rb;                 // Rigidbody for movement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Disable collision with maze walls by setting to kinematic and disabling collisions
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        // Find the player
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player not found! Ensure player has 'Player' tag.");
        }

        // Ensure at least one patrol point exists
        if (patrolPoints.Length == 0)
        {
            Debug.LogWarning("No patrol points assigned to enemy!");
        }
    }

    void Update()
    {
        if (player == null || patrolPoints.Length == 0) return;

        // Check if player is within detection radius
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        isChasing = distanceToPlayer <= detectionRadius;

        if (isChasing)
        {
            // Move towards player
            Vector2 direction = (player.position - transform.position).normalized;
            Move(direction, chaseSpeed);
        }
        else
        {
            // Patrol to the next point
            Vector2 targetPoint = patrolPoints[currentPatrolIndex].position;
            Vector2 direction = (targetPoint - (Vector2)transform.position).normalized;
            Move(direction, patrolSpeed);

            // Check if close to current patrol point
            if (Vector2.Distance(transform.position, targetPoint) < 0.2f)
            {
                // Move to next patrol point
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            }
        }
    }

    void Move(Vector2 direction, float speed)
    {
        // Move using Rigidbody2D, ignoring collisions (ghost-like behavior)
        rb.MovePosition((Vector2)transform.position + direction * speed * Time.deltaTime);
    }

    // Optional: Visualize detection radius in editor
    void OnDrawGizmos()
    {
        // Draw detection radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Draw patrol points and path
        if (patrolPoints.Length > 0)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                Gizmos.DrawWireSphere(patrolPoints[i].position, 0.3f);
                if (i < patrolPoints.Length - 1)
                {
                    Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
                }
            }
            // Connect last and first point for loop
            if (patrolPoints.Length > 1)
            {
                Gizmos.DrawLine(patrolPoints[patrolPoints.Length - 1].position, patrolPoints[0].position);
            }
        }
    }
}