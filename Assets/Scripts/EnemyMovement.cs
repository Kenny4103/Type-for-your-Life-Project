using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float detectionRadius = 5f;
    public string playerTag = "Player";

    private Transform player;
    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private Rigidbody2D rb;
    private Transform[] patrolPoints;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null) player = playerObj.transform;

        // Dynamically find patrol points under this enemy
        patrolPoints = GetComponentsInChildren<Transform>();
        patrolPoints = System.Array.FindAll(patrolPoints, t => t.name.StartsWith("PatrolPoint"));

        if (patrolPoints.Length == 0)
        {
            Debug.LogWarning("No patrol points found as children of enemy!");
        }
        else
        {
            // Sort patrol points alphabetically for consistent order
            System.Array.Sort(patrolPoints, (a, b) => a.name.CompareTo(b.name));
        }
    }

    void Update()
    {
        if (player == null || patrolPoints.Length == 0) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        isChasing = distanceToPlayer <= detectionRadius;

        if (isChasing)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Move(direction, chaseSpeed);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        Vector2 targetPoint = patrolPoints[currentPatrolIndex].position;
        Vector2 direction = (targetPoint - (Vector2)transform.position).normalized;
        Move(direction, patrolSpeed);

        if (Vector2.Distance(transform.position, targetPoint) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void Move(Vector2 direction, float speed)
    {
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
