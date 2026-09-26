using UnityEngine;

public class BossEnemyAi : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;

    Transform playerPosition;

    float runspeed;
    float moveSpeed;
    float distance;
    Rigidbody2D rb;
    Animator animator;
    bool inRange = false;

    public float Distance => distance;
    public bool InRange => inRange;

    const string walk = "Walk";
    const string run = "Run";
    void Start()
    {
        runspeed = enemyData.runspeed;
        moveSpeed = enemyData.moveSpeed;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 direction = (playerPosition.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, playerPosition.position);

        if (distance <= enemyData.attackRange && !inRange)
        {
            inRange = true;
            animator.SetBool(walk, false);
            animator.SetBool(run, false);
            rb.linearVelocity = Vector2.zero;

        }

        else if (distance > enemyData.rundistance)
        {
            animator.SetBool(run, true);
            animator.SetBool(walk, false);
            rb.linearVelocity = direction * runspeed;
        }

        else
        {
            animator.SetBool(run, false);
            animator.SetBool(walk, true);
            rb.linearVelocity = direction * moveSpeed;
        }

        if (inRange)
        {
            DecideToDo();
        }    

    }

    void DecideToDo()
    {

    }
}
