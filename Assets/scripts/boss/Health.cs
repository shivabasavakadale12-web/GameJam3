using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public BehaviourTree bt;
    public BossEnemyAi bossai;
    public bool isHurt = false;
    public bool isDead = false;

    // Keep this slightly longer than the hurt clip length
    [SerializeField] float hurtDuration = 0.64f;

    int CurrentHealth;
    Animator animator;
    Coroutine hurtRoutine;

    ClashnRecover clashnRecover;

    void Awake()
    {
        clashnRecover = FindFirstObjectByType<ClashnRecover>();
    }

    void Start()
    {
        bossai = GetComponent<BossEnemyAi>();
        bt = GetComponent<BehaviourTree>();
        animator = GetComponent<Animator>();
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
        if (isDead) return;

        if (bt.isDefending)
        {
            Debug.Log("Damage blocked (defending) at: " + Time.time);
            return;
        }

        bool lethal = amount >= CurrentHealth;

        // Hurt window protects against follow-up hits, but a killing blow always lands
        if (isHurt && !lethal)
        {
            Debug.Log("Hit ignored (hurt window) at: " + Time.time);
            return;
        }

        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            isDead = true;
            bt.InterruptAction();
            return;
        }

        if (clashnRecover != null && clashnRecover.isClash) return;

        bt.InterruptAction();

        isHurt = true;
        animator.SetTrigger("hurt");

        if (hurtRoutine != null) StopCoroutine(hurtRoutine);
        hurtRoutine = StartCoroutine(HurtOff());
    }

    IEnumerator HurtOff()
    {
        yield return new WaitForSeconds(hurtDuration);
        isHurt = false;
        hurtRoutine = null;
    }
}