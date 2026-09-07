using UnityEngine;

public class EarlyEnemyAi : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    [SerializeField] Transform playerTransform;
    float distance;
    float AttackTimer;
    Vector2 currentposition;
    Animator animator;
    Rigidbody2D rb;

    bool isMoveing;
    bool isRunning;
    BoxCollider2D Hitbox;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        AttackTimer = enemyData.attackFrequency;
        Hitbox = GetComponentInChildren<BoxCollider2D>();
        Hitbox.enabled = false;
    }

    void FixedUpdate()
    {
       currentposition = rb.position;
       Vector2 direction = (playerTransform.position - transform.position).normalized;
        distance = Vector2.Distance(playerTransform.position, transform.position);

        if (distance <= enemyData.attackRange)
        {
            isMoveing = false;
            isRunning = false;
            rb.linearVelocity = Vector2.zero;
            AttackTimer -= Time.fixedDeltaTime;
        }
        else
        {
            rb.linearVelocity = direction * enemyData.moveSpeed;
        }

        if(distance > enemyData.rundistance) 
        {
            isRunning = true;
            isMoveing = false;

        }
        else 
        {
            isMoveing = true;
            isRunning = false;

        }

        if (isMoveing)
        { 
            animator.SetBool("Iswalking", true);
            animator.SetBool("Isrunning", false);
        }
        else if (isRunning)
        {
            animator.SetBool("Isrunning", true);
            animator.SetBool("Iswalking", false);
        }

        Attackone();
    }

    void Attackone()
    {
        if (AttackTimer <= 0f && distance <= enemyData.attackRange)
        {
            animator.SetTrigger("Attack1");
            AttackTimer = enemyData.attackFrequency;
        }
    }
    public void EnableHitbox()
    {
        Hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        Hitbox.enabled = false;
    }
}
