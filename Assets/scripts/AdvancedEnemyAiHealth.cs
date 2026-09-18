using UnityEngine;

public class AdvancedEnemyAiHealth : MonoBehaviour
{
    AdvancedEnemyAI enemyScript;
    int currentHealth;
    bool isHurt = false;
    bool isdead = false;
    bool gotHit = false;
    public int hits;
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
        hits = 0;
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
        gotHit = false;
        isHurt = false;
    }

    void TakeDamage(int amount)
    {
        hits += 1;
        isHurt = true;

        if(!enemyScript.isDefending)
        {
            currentHealth -= amount;
            Debug.Log("Enemy Health: " + currentHealth);
        }

        else
        {
            Debug.Log("Enemy is defending");
        }

        if(currentHealth <= 0 && !gotHit)
        {
            
            gotHit = true;
            enemyScript.enabled = false;
            animator.SetTrigger(death);
        }

        else if(!gotHit && !enemyScript.isDefending)
        {
            gotHit = true;
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
