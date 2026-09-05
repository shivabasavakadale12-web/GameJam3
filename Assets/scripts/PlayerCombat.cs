using static UnityEngine.InputSystem.InputAction;
using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{

    BoxCollider2D[] HitBox;
    Animator animator;

    bool isDefending = false;


    void Start()
    {
        animator = GetComponent<Animator>();
        HitBox = GetComponentsInChildren<BoxCollider2D>();
        HitBox[0].enabled = false;
        HitBox[1].enabled = false;
        HitBox[2].enabled = false;
    }

    public void OnAttack1(CallbackContext context)
    {
        int randomValue = Random.Range(0, 100);
        Debug.Log("Random Value: " + randomValue);
        if (context.performed && randomValue < 50)
        {
            HitBox[0].enabled = true;
            animator.SetTrigger("OnAttack1");
        }
        else if (context.performed && randomValue >= 50)
        {
            HitBox[0].enabled = true;
            animator.SetTrigger("OnAttack2");
        }
        HitBox[1].enabled = false;
        HitBox[2].enabled = false;
    }

    public void PowerAttack(CallbackContext context)
    {
        if (context.performed)
        {
            HitBox[1].enabled = true;
            animator.SetTrigger("PowerAttack");
        }
        HitBox[0].enabled = false;
        HitBox[2].enabled = false;
    }

    public void SuperPowerAttack(CallbackContext context)

    {
        if (context.performed)
        {
            HitBox[2].enabled = true;
            animator.SetTrigger("SuperPowerAttack");
        }
        HitBox[0].enabled = false;
        HitBox[1].enabled = false;
    }

    public void OnDefend(CallbackContext context)
    {
        if (context.performed && !isDefending)
        {
            isDefending = true;
            Debug.Log("player is defending");
            StartCoroutine(ResetColliderSize());
            animator.SetTrigger("OnDefend");
        }
    }

    IEnumerator ResetColliderSize()
    {
        yield return new WaitForSeconds(1f);
        isDefending = false;

    }
}
