using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;

    int currentHealth;
    const string attack1 = "attack1";
    const string attack2 = "attack2";
    const string superPowerAttack = "superpowerattack";
    Animator animator;

    void Start()
    {
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

        Debug.Log("Enemy Health: " + currentHealth);
    }

    void hurtanimation()
    {
        animator.SetTrigger("Ishurt");
    }

    void attackone()
    {
        Debug.Log("Player hit with Attack1");
        TakeDamage(6);
    }    

    void attacktwo()
    {
        Debug.Log("Player hit with Attack2");
        TakeDamage(12);
    }    
    
    void SuperPowerAttack()
    {
        Debug.Log("Player hit with Super Power Attack");
        TakeDamage(50);
    }
    void Die()
    {
        Destroy(gameObject);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            animator.SetTrigger("IsDead");
            Invoke("Die", 1f);
        }

        else
        {
            hurtanimation();
        }
    }
}
