using UnityEngine;

public class EarlyEnemyAi : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    [SerializeField] Transform playerTransform;
    Vector2 currentposition;
    Animator animator;
    Rigidbody2D rb;

    bool isMoveing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
       currentposition = rb.position;
       Vector2 direction = (playerTransform.position - transform.position).normalized;
       rb.linearVelocity = direction * enemyData.moveSpeed;
       float distance = Vector2.Distance(playerTransform.position, transform.position);

        if (distance <= enemyData.attackRange)
        {
            isMoveing = false;
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            isMoveing = true;

        }

        if (isMoveing)
        { 
            animator.SetBool("Iswalking", true);
        }
        else
        {
            animator.SetBool("Iswalking", false);
        }

    }

}
