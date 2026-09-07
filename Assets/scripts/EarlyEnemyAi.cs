using UnityEngine;

public class EarlyEnemyAi : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    [SerializeField] Transform playerTransform;
    Vector2 currentposition;
    Animator animator;
    Rigidbody2D rb;

    bool isMoveing;
    bool isRunning;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
       currentposition = rb.position;
       Vector2 direction = (playerTransform.position - transform.position).normalized;
       float distance = Vector2.Distance(playerTransform.position, transform.position);

        if (distance <= enemyData.attackRange)
        {
            isMoveing = false;
            isRunning = false;
            rb.linearVelocity = Vector2.zero;
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
  

    }

}
