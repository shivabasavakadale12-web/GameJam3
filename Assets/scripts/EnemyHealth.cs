using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    [SerializeField] EarlyEnemyAi earlyEnemyAi;
    PlayerCombat PlayerCombat;
    int currentHealth;
    const string attack1 = "attack1";
    const string attack2 = "attack2";
    const string superPowerAttack = "superpowerattack";
    Animator animator;

    void Start()
    {
        PlayerCombat = FindFirstObjectByType<PlayerCombat>();
        animator = GetComponent<Animator>();
        currentHealth = enemyData.health;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(attack1))
        {
            attackone();
        }
        else if (collision.CompareTag(attack2))
        {
            attacktwo();
        }
        else if (collision.CompareTag(superPowerAttack))
        {
            SuperPowerAttack();
        }

    }

    void hurtanimation()
    {
        animator.SetTrigger("Ishurt");
    }

    void attackone()
    {
        TakeDamage(enemyData.playerattack1);
    }    

    void attacktwo()
    {
        TakeDamage(enemyData.playerpowerattack);
    }    
    
    void SuperPowerAttack()
    {
        TakeDamage(enemyData.playersuperpowerattack);
    }
    void Die()
    {
        Destroy(gameObject);
    }

    void TakeDamage(int damage)
    {
        if(earlyEnemyAi.IsDefending)
        {
            if (PlayerCombat.CurrentAttack == PlayerCombat.AttackType.SuperPowerAttack)
            {
                damage /= 2;
            }

            else
            {
                damage = 0;
            }

        }

        currentHealth -= damage;

        Debug.Log("Enemy health: " + currentHealth);
        Debug.Log(damage);

        if (currentHealth <= 0)
        {
            animator.SetTrigger("IsDead");
            Invoke("Die", 1f);
        }

        else if(!earlyEnemyAi.IsDefending)
        {
            hurtanimation();
        }
    }
}
