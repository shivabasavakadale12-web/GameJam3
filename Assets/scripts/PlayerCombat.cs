using static UnityEngine.InputSystem.InputAction;
using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{

    BoxCollider2D[] HitBox;
    Animator animator;

    bool isDefending = false;
    bool isAttacking = false;


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
        if (context.performed && randomValue < 50 && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("OnAttack1");
        }
        else if (context.performed && randomValue >= 50 && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("OnAttack2");
        }
    }

    public void PowerAttack(CallbackContext context)
    {
        if (context.performed && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("PowerAttack");
        }
    }
    
    public void SuperPowerAttack(CallbackContext context)
    {
        if (context.performed && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("SuperPowerAttack");
        }
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

    public void EnableAttackHitbox()
    {
        HitBox[0].enabled = true;
    }

    public void DisableAttackHitbox()
    {
        HitBox[0].enabled = false;
    }

    public void EnablePowerAttackHitbox()
    {
        HitBox[1].enabled = true;
    }
    public void DisablePowerAttackHitbox()
    {
        HitBox[1].enabled = false;
    }

    public void EnableSuperPowerAttackHitbox()
    {
        HitBox[2].enabled = true;
    }

    public void DisableSuperPowerAttackHitbox()
    {
        HitBox[2].enabled = false;
    }

    public void attackfinished()
    {
        isAttacking = false;
    }
    IEnumerator ResetColliderSize()
    {
        yield return new WaitForSeconds(1f);
        isDefending = false;

    }
}
