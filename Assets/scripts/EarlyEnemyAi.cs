using UnityEngine;
using System.Collections;
public class EarlyEnemyAi : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    [SerializeField] Transform playerTransform;
    PlayerCombat playerCombat;
    float distance;
    bool ReactTOPlayer = false;
    bool isDefending = false;
    bool iscounterattack = false;
    public bool IScounterattack => iscounterattack;
    Vector2 currentposition;
    Animator animator;
    Rigidbody2D rb;

    bool isMoveing;
    bool isRunning;
    bool isAttacking = false;
    public bool IsDefending => isDefending;
    BoxCollider2D[] Hitbox;
    const string attack1 = "Attack1";
    const string attack2 = "Attack2";
    const string attack3 = "Attack3";

    void Start()
    {
        playerCombat = playerTransform.GetComponent<PlayerCombat>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Hitbox = GetComponentsInChildren<BoxCollider2D>();
        Hitbox[0].enabled = false;
        Hitbox[1].enabled = false;
        Hitbox[2].enabled = false;
    }

    void FixedUpdate()
    {
        currentposition = rb.position;
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        distance = Vector2.Distance(playerTransform.position, transform.position);

        if (distance <= enemyData.attackRange)
        {
            isMoveing = false;
            isRunning = false;
            rb.linearVelocity = Vector2.zero;
        }

        else if (distance > enemyData.rundistance)
        {
            isRunning = true;
            isMoveing = false;
            rb.linearVelocity = direction * enemyData.moveSpeed;
        }
        else
        {
            isMoveing = true;
            isRunning = false;
            rb.linearVelocity = direction * enemyData.moveSpeed;
        }

        AnimationStates();
        AttacknDefendState();

    }

    private void AttacknDefendState()
    {
        if (playerCombat.IsAttacking && !ReactTOPlayer)
        {
            ReactTOPlayer = true;
            if (playerCombat.CurrentAttack == PlayerCombat.AttackType.Attack1 ||
                playerCombat.CurrentAttack == PlayerCombat.AttackType.Attack2)
            {
                StartCoroutine(EnemyReactionToPlayerAttack());
            }

            else if (playerCombat.CurrentAttack == PlayerCombat.AttackType.PowerAttack)
            {
                StartCoroutine(EnemyReactionToPlayerAttack());
            }

            else if (playerCombat.CurrentAttack == PlayerCombat.AttackType.SuperPowerAttack)
            {
                StartCoroutine(EnemyReactionToPlayerAttack());
            }
        }

        if (!playerCombat.IsAttacking)
        {
            ReactTOPlayer = false;
        }

        if (playerCombat.IsDefending)
        {
            Debug.Log("Player is defending!");
        }
    }

    void AnimationStates()
    {

        if (isMoveing)
        {
            animator.SetBool("Iswalking", true);
            animator.SetBool("Isrunning", false);
        }
        else if (isRunning)
        {
            animator.SetBool("Isrunning", true);
            animator.SetBool("Iswalking", false);
        }
        else
        {
            animator.SetBool("Iswalking", false);
            animator.SetBool("Isrunning", false);
        }

    }

    IEnumerator EnemyReactionToPlayerAttack()
    {
        yield return new WaitForSeconds(enemyData.reactionTime);

        if (distance > enemyData.attackRange ||
            isAttacking ||
            isDefending)
        {
            yield break;
        }

        float defenseRoll = Random.Range(0f, 100f);

        if (defenseRoll <= enemyData.defenseTendency)
        {
            isDefending = true;
            animator.SetTrigger("Defend");
            yield break;
        }

        float counterRoll = Random.Range(0f, 100f);

        if (counterRoll <= enemyData.counterAttackTendency)
        {
            isAttacking = true;
            iscounterattack = true;
            animator.SetTrigger(attack3);
            yield break;
        }

        float aggressionRoll = Random.Range(0f, 100f);

        if (aggressionRoll <= enemyData.aggression)
        {
            isAttacking = true;

            int randomAttack = Random.Range(0, 100);

            if (randomAttack < 50)
            {
                animator.SetTrigger(attack1);
            }
            else
            {
                animator.SetTrigger(attack2);
            }
        }
    }
    public void EnableHitboxone()
    {
        Hitbox[0].enabled = true;
    }

    public void DisableHitboxone()
    {
        Hitbox[0].enabled = false;
        isAttacking = false;
    }

    public void Enablehitboxtwo()
    {
        Hitbox[1].enabled = true;
    }

    public void Disablehitboxtwo()
    {
        Hitbox[1].enabled = false;
        isAttacking = false;
    }

    public void Enablehitboxthree()
    {
        Hitbox[2].enabled = true;
    }

    public void Disablehitboxthree()
    {
        Hitbox[2].enabled = false;
        iscounterattack = false;
        isAttacking = false;
    }

    public void DefendFinished()
    {
        isDefending = false;
    }
}
