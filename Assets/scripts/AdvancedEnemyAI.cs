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
        Vector2 direction =
         (playerTransform.position - transform.position).normalized;

        distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance <= enemyData.attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            Debug.Log(distance.ToString() + " / " + enemyData.attackRange);
        }
        else
        {
            rb.linearVelocity = direction * enemyData.moveSpeed;
            Debug.Log(distance.ToString() + " / " + enemyData.attackRange);
        }

    }
}
