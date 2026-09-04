using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movespeed = 5f;
    [SerializeField] Animator animator;
    float sprintmultiplier = 1.5f;
    Vector2 movement;
    Rigidbody2D rb;
    bool isSprint = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnPlayermovement(CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        Debug.Log("PlayerMovement: " + movement);
    }

    public void OnSprint(CallbackContext context)
    {
        isSprint = context.ReadValueAsButton();
        if (isSprint)
        {
           animator.SetBool("Run", true);
            movespeed *= sprintmultiplier;
        }
        else
        {
            animator.SetBool("Run", false);
            movespeed = 5f;
        }
    }

    void FixedUpdate()
    {
        if(movement.x != 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        Vector2 currentposition = rb.position;
        Vector2 direction = new Vector2(movement.x * movespeed , 0);

        rb.MovePosition(currentposition + direction * Time.fixedDeltaTime);
    }
}
