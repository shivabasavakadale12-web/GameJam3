using UnityEngine;
using System.Collections;

public class playerHealth : MonoBehaviour
{
    const string enemyattack1 = "enemyattack1";
    int health;
    bool ishurt = false;
    public bool IsHurt => ishurt;
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
        AdvancedEnemyAI enemy = collision.GetComponentInParent<AdvancedEnemyAI>();

        if (enemy == null)
            return;

        if (playerCombat.IsDefending)
        {
            Debug.Log("Player blocked the attack.");
            return;
        }

        if (collision.CompareTag(enemyattack1))
        {
            playerCombat.CancelAttack();
            playerCombat.cancleDefending();
            playerCombat.LockActions(0.35f);

            ishurt = true;
            animator.SetTrigger("IsHurt");

            TakeDamage(enemy.Enemydata.attack1);
        }

        else if (collision.CompareTag("enemyattack2"))
        {
            playerCombat.CancelAttack();
            playerCombat.cancleDefending();
            playerCombat.LockActions(0.35f);

            ishurt = true;
            animator.SetTrigger("IsHurt");

            TakeDamage(enemy.Enemydata.attack2);
        }

        else if (collision.CompareTag("enemyattack3"))
        {
            playerCombat.CancelAttack();
            playerCombat.cancleDefending();
            playerCombat.LockActions(0.35f);

            ishurt = true;
            animator.SetTrigger("IsHurt");

            TakeDamage(enemy.Enemydata.attack3);
        }

        else if (collision.CompareTag("enemyattack4"))
        {
            playerCombat.CancelAttack();
            playerCombat.cancleDefending();
            playerCombat.LockActions(0.35f);

            ishurt = true;
            animator.SetTrigger("IsHurt");

            TakeDamage(enemy.Enemydata.counterattackdamage);
        }
    }

    public void Hurtstatedone()
    {
        ishurt = false;
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
            StartCoroutine(Deadroutine());
        }
    }

    IEnumerator Deadroutine()
    {
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
