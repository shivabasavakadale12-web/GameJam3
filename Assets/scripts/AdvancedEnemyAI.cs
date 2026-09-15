using UnityEngine;
using System.Collections;


public class AdvancedEnemyAI : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    Transform playerTransform;
    Rigidbody2D rb;
    Animator animator;
    float distance;
    BoxCollider2D[] hitbox;
    const string walk = "Walk";
    const string run = "Run";

    AdvancedEnemyState currentstate;
    AdvabcedEnemyAttackStatee attackstate;

    public Animator Animator => animator;
    public Rigidbody2D Rigidbody => rb;
    public EnemyData Enemydata => enemyData;
    public Transform PlayerTransform => playerTransform;
    public float Distance => distance;


    void Start()
    {
        attackstate = new AdvabcedEnemyAttackStatee(this);
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


         if (distance <= enemyData.attackRange)
        {
            ChangeState(currentstate);
            animator.SetBool(walk, false);
            animator.SetBool(run, false);

            rb.linearVelocity = Vector2.zero;

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

    }

    void ChangeState(AdvancedEnemyState newstate)
    {
        currentstate.Exit();
        currentstate = newstate;
        currentstate.Enter();
    }
}
