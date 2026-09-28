using UnityEngine;

public class Health : MonoBehaviour
{
    BehaviourTree bt;

    public bool isHurt = false;
    public bool isDead = false;

    int CurrentHealth;
     void Start()
     {
        bt = GetComponent<BehaviourTree>();
        CurrentHealth = bt.enemyData.health;
     }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("attack1"))
        {
            takeDamage(bt.enemyData.playerattack1);
        }

        if (collision.gameObject.CompareTag("attack2"))
        {
            takeDamage(bt.enemyData.playerpowerattack);
        }

        if (collision.gameObject.CompareTag("superpowerattack"))
        {
            takeDamage(bt.enemyData.playersuperpowerattack);
        }


    }
    void takeDamage(int amount)
    {
        CurrentHealth -= amount;

        if(CurrentHealth <= 0)
        {
            isDead = true;
        }
        else
        {
            isHurt = true;
        }
    }
}
