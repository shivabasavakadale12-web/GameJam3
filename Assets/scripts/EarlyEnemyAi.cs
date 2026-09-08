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
    bool isAttacking = false;
    BoxCollider2D[] Hitbox;
    const string attack1 = "Attack1";
    const string attack2 = "Attack2";
    const string attack3 = "Attack3";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        AttackTimer = enemyData.attackFrequency;
        Hitbox = GetComponentsInChildren<BoxCollider2D>();
        Hitbox[0].enabled = false;
        Hitbox[1].enabled = false;
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

        else if (distance > enemyData.rundistance)
        {
            isRunning = true;
            isMoveing = false;
            rb.linearVelocity = direction * enemyData.moveSpeed;
        }
        else
        {
            isMoveing = true;
            isRunning = false;
            rb.linearVelocity = direction * enemyData.moveSpeed;
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
        else
        {
            animator.SetBool("Iswalking", false);
            animator.SetBool("Isrunning", false);
        }

        Attackone();
    }

    void Attackone()
    {
        int randomAttack = Random.Range(0, 100);

        if (AttackTimer <= 0f && distance <= enemyData.attackRange && !isAttacking)
        {
            isAttacking = true;
            if (randomAttack < 50)
            {
                animator.SetTrigger(attack1);
                Debug.Log("Enemy Attack1");
            }
            else if (randomAttack > 50 && randomAttack < 80) 
            {
                animator.SetTrigger(attack2);
                Debug.Log("Enemy Attack2");
            }
            else
            {
                animator.SetTrigger(attack3);
                Debug.Log("Enemy Attack3");
            }

            AttackTimer = enemyData.attackFrequency;
        }
    }
    public void EnableHitboxone()
    {
        Hitbox[0].enabled = true;
    }

    public void DisableHitboxone()
    {
        Hitbox[0].enabled = false;
        isAttacking = false;
    }

    public void Enablehitboxtwo()
    {
        Hitbox[1].enabled = true;
    }

    public void Disablehitboxtwo()
    {
        Hitbox[1].enabled = false;
        isAttacking = false;
    }

    public void Enablehitboxthree()
    {
        Hitbox[2].enabled = true;
    }

    public void Disablehitboxthree()
    {
        Hitbox[2].enabled = false;
        isAttacking = false;
    }
}
