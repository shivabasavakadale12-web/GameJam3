using static UnityEngine.InputSystem.InputAction;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnAttack1(CallbackContext context)
    {
        int randomValue = Random.Range(0, 100);
        Debug.Log("Random Value: " + randomValue);
        if (context.performed && randomValue < 50)
        {
            animator.SetTrigger("OnAttack1");
        }
        else if (context.performed && randomValue >= 50)
        {
            animator.SetTrigger("OnAttack2");
        }
    }

    public void PowerAttack(CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("PowerAttack");
        }
    }

    public void SuperPowerAttack(CallbackContext context)

    {
        if (context.performed)
        {
            animator.SetTrigger("SuperPowerAttack");
        }

    }

}
