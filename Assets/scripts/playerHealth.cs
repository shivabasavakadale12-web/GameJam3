using UnityEngine;

public class playerHealth : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
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
        if (collision.CompareTag(enemyattack1))
        {
            if (playerCombat.IsDefending)
            {
                Debug.Log("player health is " +health);
                return;
            }
           
               playerCombat.CancelAttack();
               playerCombat.cancleDefending();
               ishurt = true;
               animator.SetTrigger("IsHurt");
               TakeDamage(enemyData.attack1);
            
        }

        else if (collision.CompareTag("enemyattack2"))
        {
            if (playerCombat.IsDefending)
            {
                Debug.Log("player health is " +health);
                return;
            }

                playerCombat.CancelAttack();
                playerCombat.cancleDefending();
                ishurt = true;
                animator.SetTrigger("IsHurt");
                TakeDamage(enemyData.attack2);
            
        }
        else if (collision.CompareTag("enemyattack3"))
        {
            if (playerCombat.IsDefending)
            {
                Debug.Log("player health is " +health);
                return;
            }

                playerCombat.CancelAttack();
                playerCombat.cancleDefending();
                ishurt = true;
                animator.SetTrigger("IsHurt");
                TakeDamage(enemyData.attack3);
            
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

        }
    }
}
