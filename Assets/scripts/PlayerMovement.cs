using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movespeed = 5f;
    [SerializeField] Animator animator;
    float sprintmultiplier = 1.5f;
    Vector2 movement;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    bool isSprint = false;
    bool isjump = false;

    float cooldown = 0f;

    float currentspeed;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currentspeed = movespeed;
    }

    public void OnPlayermovement(CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnSprint(CallbackContext context)
    {
        isSprint = context.ReadValueAsButton();
        
    }

    public void OnJump(CallbackContext context)
    {
        isjump = context.ReadValueAsButton();
    }

    void FixedUpdate()
    {
        if(movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (isSprint && movement.x != 0)
        {
            animator.SetBool("Run", true);
            currentspeed = movespeed * sprintmultiplier;
        }
        else
        {
            animator.SetBool("Run", false);
            currentspeed = movespeed;
        }

        if (movement.x != 0 && !isSprint)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        if (movement.x == 0 && !isSprint)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }

        if (cooldown > 0f)
        {
            cooldown -= Time.fixedDeltaTime;
        }

        if (isjump && cooldown <= 0f)
        {
            animator.SetTrigger("Jump");
            cooldown += 1f;
        }

        Vector2 currentposition = rb.position;
        Vector2 direction = new Vector2(movement.x * currentspeed , 0);

        rb.MovePosition(currentposition + direction * Time.fixedDeltaTime);
    }
}
