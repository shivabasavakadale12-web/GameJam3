using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] ClashnRecover clashnRecover;
    public BehaviourTree bt;
    public BossEnemyAi bossai;
    bool stClash = false;
    public bool StClash => stClash;
    public bool isHurt = false;
    public bool isDead = false;

    int clashCounter = 0;
    int CurrentHealth;
     void Start()
     {
        bossai = GetComponent<BossEnemyAi>();
        bt = GetComponent<BehaviourTree>();
        CurrentHealth = bt.enemyData.health;
     }

    void OnTriggerEnter2D(Collider2D collision)
    {
        clashCounter++;

        if (clashCounter >= clashnRecover.clashThreshold)
        {
            stClash = true;
            Debug.Log("Clash!");
            clashCounter = 0;
        }

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
        if(!bt.isDefending)
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

        else
        {
            Debug.Log("Damage blocked — isDefending stuck at: " + Time.time);
        }
    }
}
