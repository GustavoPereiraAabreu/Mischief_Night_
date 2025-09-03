using UnityEngine;

public enum EnemyState
{
    Patrolling,
    Chasing
}

public class EnemieFollowing : MonoBehaviour
{
    [Header("Behavior")]
    [SerializeField] private float speed = 9f;
    [SerializeField] private float stoppingDistance = 1f;

    [Header("Patrol Points")]
    [SerializeField] private Transform[] patrolPoints;

    [Header("Player Reference")]
    [SerializeField] private HideInside playerHidingScript; // Arraste o Player aqui no Inspector! //

    private Transform target;
    private Rigidbody2D rb;
    private int currentPatrolPointIndex;

    void Awake()
    {
        if (playerHidingScript == null)
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null)
                playerHidingScript = playerGO.GetComponent<HideInside>();
        }

        if (playerHidingScript == null)
            Debug.LogError("Script HideInside não encontrado no Player!");

        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("Rigidbody2D não encontrado no inimigo!");

        target = playerHidingScript != null ? playerHidingScript.transform : null;
        currentPatrolPointIndex = patrolPoints.Length > 0 ? 0 : -1;
    }

    void FixedUpdate()
    {
        if (playerHidingScript == null || rb == null) return;

        if (playerHidingScript.isHidden)
        {
            Patrol();
        }
        else
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        if (target == null) return;
        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    void Patrol()
    {


        if (patrolPoints.Length == 0 || currentPatrolPointIndex == -1) return;

        Transform targetPatrolPoint = patrolPoints[currentPatrolPointIndex];
        Vector2 direction = ((Vector2)targetPatrolPoint.position - rb.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        if (Vector2.Distance(rb.position, targetPatrolPoint.position) < stoppingDistance)
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }
    }
}
