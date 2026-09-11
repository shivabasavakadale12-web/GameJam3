using static UnityEngine.InputSystem.InputAction;
using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    BoxCollider2D[] HitBox;
    Animator animator;
    bool isDefending = false;
    bool isAttacking = false;
    public bool IsDefending => isDefending;
    public bool IsAttacking => isAttacking;
    public AttackType CurrentAttack {  get; private set; }
    const string attack1 = "OnAttack1";
    const string attack2 = "OnAttack2";
    const string powerAttack = "PowerAttack";
    const string superPowerAttack = "SuperPowerAttack";

    public enum AttackType
    {
        None,
        Attack1,
        Attack2,
        PowerAttack,
        SuperPowerAttack,
    }

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
        if (context.performed && !isAttacking)
        {
            isAttacking = true;

            int randomValue = Random.Range(0, 100);

            if (randomValue < 50)
            {
                CurrentAttack = AttackType.Attack1;
                animator.SetTrigger(attack1);
            }
            else
            {
                CurrentAttack = AttackType.Attack2;
                animator.SetTrigger(attack2);
            }

        }
       
    }

    public void PowerAttack(CallbackContext context)
    {
        if (context.performed && !isAttacking)
        {
            CurrentAttack = AttackType.PowerAttack;
            isAttacking = true;
            animator.SetTrigger(powerAttack);
        }
    }
    
    public void SuperPowerAttack(CallbackContext context)
    {
        if (context.performed && !isAttacking)
        {
            CurrentAttack = AttackType.SuperPowerAttack;
            isAttacking = true;
            animator.SetTrigger(superPowerAttack);
        }
    }

    public void OnDefend(CallbackContext context)
    {
        Debug.Log(isDefending);
        if (context.performed && !isDefending)
        {
            Debug.Log("player is defending");
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

    public void isdefending()
    {
        isDefending = true;
    }

    public void defendfinished()
    {
        isDefending = false;
    }

    public void cancleDefending()
    {
        isDefending = false;
    }

    public void CancelAttack()
    {
        isAttacking = false;
        CurrentAttack = AttackType.None;
    }

}
