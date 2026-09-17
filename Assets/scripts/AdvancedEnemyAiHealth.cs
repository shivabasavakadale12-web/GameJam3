using UnityEngine;

public class AdvancedEnemyAiHealth : MonoBehaviour
{
    AdvancedEnemyAI enemyScript;
    int currentHealth;
    bool isHurt = false;
    bool isdead = false;
    public bool IsHurt => isHurt;
    const string attack1 = "attack1";
    const string attack2 = "attack2";
    const string superPowerAttack = "superpowerattack";
    const string hurt = "hurt";
    const string death = "Death";

    Animator animator;

    [SerializeField]  EnemyData enemyData;

     void Start()
     {
        enemyScript = GetComponent<AdvancedEnemyAI>();
        currentHealth = enemyData.health;
        animator = GetComponent<Animator>();
     }

     void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.CompareTag(attack1))
        {
            TakeDamage(enemyData.playerattack1);
        }


        else if (collision.gameObject.CompareTag(attack2))
        {
            TakeDamage(enemyData.playerpowerattack);
        }


        else if (collision.gameObject.CompareTag(superPowerAttack))
        {
            TakeDamage(enemyData.playersuperpowerattack);  
        }
    }
    public void HurtDone()
    {
        isHurt = false;
    }

    void TakeDamage(int amount)
    {
        isHurt = true;
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            enemyScript.enabled = false;
            animator.SetTrigger(death);
        }

        else
        {
            animator.SetTrigger(hurt);
        }
    }

    void Update()
    {
        if (isdead) dead();
    }

    public void Isdead()
    {
        isdead = true;
    }
    void dead()
    {
        Destroy(gameObject);
    }
}
