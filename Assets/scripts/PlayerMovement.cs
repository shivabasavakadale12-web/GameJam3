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

    const string Runstring = "Run";
    const string Walkstring = "Walk";

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
           animator.SetTrigger(Runstring);
            movespeed *= sprintmultiplier;
        }
        else
        {
            animator.SetTrigger(Walkstring);
            movespeed = 5f;
        }
    }

    void FixedUpdate()
    {
        Vector2 currentposition = rb.position;
        Vector2 direction = new Vector2(movement.x * movespeed , 0);

        rb.MovePosition(currentposition + direction * Time.fixedDeltaTime);
    }
}
