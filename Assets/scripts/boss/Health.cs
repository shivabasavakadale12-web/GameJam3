using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public BehaviourTree bt;
    public BossEnemyAi bossai;
    public bool isHurt = false;
    public bool isDead = false;

    int CurrentHealth;

    ClashnRecover clashnRecover;

    void Awake()
    {
        clashnRecover = FindFirstObjectByType<ClashnRecover>();
    }

    void Start()
    {
        bossai = GetComponent<BossEnemyAi>();
        bt = GetComponent<BehaviourTree>();
        CurrentHealth = bt.enemyData.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        bool isPlayerAttack =
            collision.gameObject.CompareTag("attack1") ||
            collision.gameObject.CompareTag("attack2") ||
            collision.gameObject.CompareTag("superpowerattack");

        if (!isPlayerAttack) return;

        if (clashnRecover != null)
            clashnRecover.ReportHit(true, gameObject);

        if (collision.gameObject.CompareTag("attack1"))
            takeDamage(bt.enemyData.playerattack1);
        else if (collision.gameObject.CompareTag("attack2"))
            takeDamage(bt.enemyData.playerpowerattack);
        else if (collision.gameObject.CompareTag("superpowerattack"))
            takeDamage(bt.enemyData.playersuperpowerattack);
    }

    void takeDamage(int amount)
    {
        if (!bt.isDefending && !isHurt)
        {
            CurrentHealth -= amount;

            if (CurrentHealth <= 0)
                isDead = true;
            else
            {
                isHurt = true;
                StartCoroutine(HurtOff());
            }

        }
        else
        {
            Debug.Log("Damage blocked — isDefending stuck at: " + Time.time);
        }
    }
    IEnumerator HurtOff()
    {
        yield return new WaitForSeconds(0.64f);
        isHurt = false;
    }
}
