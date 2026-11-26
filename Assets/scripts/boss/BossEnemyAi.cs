using UnityEngine;
using System.Collections;

public class BossEnemyAi : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    PowerAttackController PowerAttackController;
    Transform playerPosition;
    float runspeed;
    float moveSpeed;
    float distance;
    Rigidbody2D rb;
    Animator animator;
    bool inRange = false;
    bool LockAction = false;
    public float Distance => distance;
    public bool InRange => inRange;
    Vector2 direction;
    public bool IsNearBoundary { get; private set; }
    const string walk = "Walk";
    const string run = "Run";

    public void LockMovement(float duration)
    {
        StartCoroutine(LockMovementRoutine(duration));
    }

    IEnumerator LockMovementRoutine(float duration)
    {
        LockAction = true;
        yield return new WaitForSeconds(duration);
        LockAction = false;
    }

    void Start()
    {
        runspeed = enemyData.runspeed;
        moveSpeed = enemyData.moveSpeed;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        PowerAttackController = GetComponent<PowerAttackController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (LockAction || PowerAttackController.IsCounterAttackRunning) return;

        if (playerPosition == null) return;
         direction = (playerPosition.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, playerPosition.position);

        if (distance <= enemyData.attackRange)
        {
            KeepDistance();
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

        if (distance <= enemyData.attackRange && !inRange)
        {
            inRange = true;
        }
        else if (distance > enemyData.attackRange && inRange)
        {
            inRange = false;
        }   

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("EndCollider"))
        {
            Debug.Log("fuck youuu");
            IsNearBoundary = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("EndCollider"))
        {
            IsNearBoundary = false;
        }
    }
    void KeepDistance()
    {
        if(distance < enemyData.keepDistance && !IsNearBoundary)
        {
            animator.SetBool(walk, true);
            animator.SetBool(run, false);
            rb.linearVelocity = -direction * moveSpeed;

        }

        else
        {

        animator.SetBool(walk, false);
        animator.SetBool(run, false);
        rb.linearVelocity = Vector2.zero;
        }
    }    
}
