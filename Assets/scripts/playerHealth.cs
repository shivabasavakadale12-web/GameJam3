using UnityEngine;

public class playerHealth : MonoBehaviour
{
    int health;
    const string enemyattack1 = "enemyattack1";

    Animator animator;
    PlayerCombat playerCombat;
    PlayerMovement playerMovement;
    CapsuleCollider2D playercollider;

    void Start()
    {
        health = 100;
        animator = GetComponent<Animator>();
        playerCombat = GetComponent<PlayerCombat>();
        playerMovement = GetComponent<PlayerMovement>();
        playercollider = GetComponent<CapsuleCollider2D>();
        playercollider.enabled = true;
        playerMovement.enabled = true;
        playerCombat.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyattack1))
        {
            if (playerCombat.isDefending)
            {
                Debug.Log("player defended the attack1");
                return;
            }
            else
            {
               animator.SetTrigger("IsHurt");
               TakeDamage(7);
            }
        }

        else if (collision.CompareTag("enemyattack2"))
        {
            if (playerCombat.isDefending)
            {
                Debug.Log("player defended the attack2");
                return;
            }
            else
            {
             animator.SetTrigger("IsHurt");
             TakeDamage(10);
            }
        }
        else if (collision.CompareTag("enemyattack3"))
        {
            if (playerCombat.isDefending)
            {
                Debug.Log("player defended the attack3");
                return;
            }
            else
            {
                animator.SetTrigger("IsHurt");
                TakeDamage(12);
            }
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player Health: " + health);
        if (health <= 0)
        {
            playercollider.enabled = false;
            playerMovement.enabled = false;
            playerCombat.enabled = false;
            animator.SetTrigger("IsDead");

        }
    }
}
