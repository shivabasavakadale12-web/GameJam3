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
    void Start()
    {
        runspeed = enemyData.runspeed;
        moveSpeed = enemyData.moveSpeed;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (playerPosition.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, playerPosition.position);

        if (distance <= enemyData.attackRange)
        {
            rb.linearVelocity = Vector2.zero;

        }

        else if (distance > enemyData.rundistance)
        {

            rb.linearVelocity = direction * runspeed;
        }

        else
        {
            rb.linearVelocity = direction * moveSpeed;
        }


    }
}
