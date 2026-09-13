using UnityEngine;

public class AdvancedEnemyAI : MonoBehaviour
{
    [SerializeField] float MoveSpeed;
    [SerializeField] EnemyData enemyData;
    Transform playerTransform;
    Rigidbody2D rb;
    Animator animator;
    float distance;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector2 currentposition = rb.position;
        Vector2 direction = ( playerTransform.position - transform.position).normalized;
        distance = Vector2.Distance(currentposition, direction);

        if (distance <= enemyData.attackRange)
        {
            rb.linearVelocity = Vector2.zero;
        }

        else
        {
            rb.linearVelocity = direction * enemyData.moveSpeed;
        }

    }
}
