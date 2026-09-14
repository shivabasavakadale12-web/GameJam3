using UnityEngine;

public class AdvancedEnemyAI : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    Transform playerTransform;
    Rigidbody2D rb;
    Animator animator;
    float distance;
    float runspeed = 1.7f;

    BoxCollider2D[] hitbox;

    bool isAttacking = false;
    bool isDefending = false;
    const string walk = "Walk";
    const string run = "Run";
    void Start()
    {
        hitbox = GetComponentsInChildren<BoxCollider2D>();
        hitbox[0].enabled = false;
        hitbox[1].enabled = false;
        hitbox[2].enabled = false;
        hitbox[3].enabled = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector2 direction =
         (playerTransform.position - transform.position).normalized;

        distance = Vector2.Distance(transform.position, playerTransform.position);


        if (isAttacking)
        {
            rb.linearVelocity = Vector2.zero;
        }

        else if (distance <= enemyData.attackRange)
        {
            animator.SetBool(walk, false);
            animator.SetBool(run, false);

            rb.linearVelocity = Vector2.zero;

            isAttacking = true;
            animator.SetTrigger("Attack1");
        }

        else if (distance > enemyData.rundistance)
        {
            animator.SetBool(run, true);
            animator.SetBool(walk, false);

            rb.linearVelocity = direction * enemyData.runspeed;
        }

        else
        {
            animator.SetBool(walk, true);
            animator.SetBool(run, false);

            rb.linearVelocity = direction * enemyData.moveSpeed;
        }

        Debug.Log(distance + " / " + enemyData.attackRange);
    }

    void hitboxoneenable()
    {
        hitbox[0].enabled = true;
    }

    void hitboxonedisable()
    {
        hitbox[0].enabled = false;
        isAttacking = false;
    }
}
