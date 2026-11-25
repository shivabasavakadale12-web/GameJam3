
using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movespeed = 5f;
    [SerializeField] Animator animator;
    [SerializeField] float minimumEnemyDistance = 2f;

    float sprintmultiplier = 1.5f;
    Vector2 movement;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    bool isSprint = false;
    bool isjump = false;
    bool LockAction = false;

    float cooldown = 0f;
    float currentspeed;

    IEnemy currentEnemy;

    public void SetEnemy(IEnemy enemy)
    {
        currentEnemy = enemy;
    }

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
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currentspeed = movespeed;
    }

    public void OnPlayermovement(CallbackContext context)
    {
        if (LockAction) return;

        movement = context.ReadValue<Vector2>();
    }

    public void OnSprint(CallbackContext context)
    {
        if (LockAction) return;

        isSprint = context.ReadValueAsButton();
    }

    public void OnJump(CallbackContext context)
    {
        if (LockAction) return;

        isjump = context.ReadValueAsButton();
    }

    void FixedUpdate()
    {
        if (LockAction) return;

        Vector2 currentPosition = rb.position;
        float horizontal = movement.x;

        currentspeed = isSprint
            ? movespeed * sprintmultiplier
            : movespeed;
        if (currentEnemy != null &&
            currentEnemy.EnemyTransform != null &&
            horizontal != 0)
        {
            float enemyX = currentEnemy.EnemyTransform.position.x;

            float currentDistance =
                Mathf.Abs(currentPosition.x - enemyX);

            float nextX = currentPosition.x +
                          horizontal * currentspeed * Time.fixedDeltaTime;

            float nextDistance =
                Mathf.Abs(nextX - enemyX);

            if (nextDistance < minimumEnemyDistance &&
                nextDistance < currentDistance)
            {
                horizontal = 0f;
            }
        }

        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }

    
        animator.SetBool("Run", isSprint && horizontal != 0);

        animator.SetBool("Walk", !isSprint && horizontal != 0);

        if (cooldown > 0f)
        {
            cooldown -= Time.fixedDeltaTime;
        }

        if (isjump && cooldown <= 0f)
        {
            animator.SetTrigger("Jump");
            cooldown += 1f;
        }

        Vector2 direction = new Vector2(
            horizontal * currentspeed,
            0f
        );

        rb.MovePosition(
            currentPosition + direction * Time.fixedDeltaTime
        );
    }
}
