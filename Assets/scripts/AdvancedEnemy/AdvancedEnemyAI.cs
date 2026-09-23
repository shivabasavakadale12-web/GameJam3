using UnityEngine;


public class AdvancedEnemyAI : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;

    PlayerCombat player;
    Transform playerTransform;
    Rigidbody2D rb;
    Animator animator;
    bool wasPlayerAttacking;
    bool isPlayerAttacking;
    public bool isAttacking = false;
    public bool isDefending = false;
    float distance;
    public float moveSpeed;
    public float runspeed;
    public float attackfrequency;
    float reactiontime;
    public BoxCollider2D[] hitbox;
    const string walk = "Walk";
    const string run = "Run";
    const string defend = "Defend";
    bool hasWalkParam;
    bool hasDefendParam;

    public int CurrentHitboxIndex { get; set; }

    AdvancedEnemyAiHealth health;

    AdvancedEnemyState currentstate;
    AdvabcedEnemyAttackStatee attackstate;
    AdvancedEnemyAiDefenseState defenseState;
    AdvancedEnemyAiCounterAttackState CounterAttackState;

    public Animator Animator => animator;
    public Rigidbody2D Rigidbody => rb;
    public EnemyData Enemydata => enemyData;
    public AdvancedEnemyAiHealth Health => health;
    public Transform PlayerTransform => playerTransform;
    public float Distance => distance;


    void Start()
    {
        reactiontime = 0f;
        attackfrequency = enemyData.attackFrequency;
        moveSpeed = enemyData.moveSpeed;
        runspeed = enemyData.runspeed;

        CounterAttackState = new AdvancedEnemyAiCounterAttackState(this);
        defenseState = new AdvancedEnemyAiDefenseState(this);
        attackstate = new AdvabcedEnemyAttackStatee(this);


        health = GetComponent<AdvancedEnemyAiHealth>();
        hitbox = GetComponentsInChildren<BoxCollider2D>();

        foreach (var hb in hitbox)
        {
            hb.enabled = false;
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        player = playerTransform.GetComponent<PlayerCombat>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        foreach (var p in animator.parameters)
        {
            if (p.name == walk) hasWalkParam = true;
            if (p.name == defend) hasDefendParam = true;
        }
    }

    void FixedUpdate()
    {
        Vector2 direction =
         (playerTransform.position - transform.position).normalized;

        distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance <= enemyData.attackRange)
        {
            if (hasWalkParam) animator.SetBool(walk, false);
            animator.SetBool(run, false);

            rb.linearVelocity = Vector2.zero;

            if (currentstate == null)
            {
                ChangeToAttack();
            }

            DecideWhatToDo();
        }

        else if (distance > enemyData.rundistance)
        {
            animator.SetBool(run, true);
            if (hasWalkParam) animator.SetBool(walk, false);

            rb.linearVelocity = direction * runspeed;
        }

        else
        {
            if (hasWalkParam) animator.SetBool(walk, true);
            animator.SetBool(run, false);

            rb.linearVelocity = direction * moveSpeed;
        }

        if (currentstate != null)
        {
            currentstate.Update();
        }

    }

    public void ChangeState(AdvancedEnemyState newstate)
    {
        if (currentstate == newstate)
            return;

        if (currentstate != null)
        {
            currentstate.Exit();
        }

        currentstate = newstate;
        currentstate.Enter();
    }

    public void ChangeToAttack()
    {
        ChangeState(attackstate);
    }

    public void ChangeToDefense()
    {
        ChangeState(defenseState);
    }

    public void ChangeToCounter()
    {
        ChangeState(CounterAttackState);
    }

    public void SetCurrentHitbox(int index)
    {
        CurrentHitboxIndex = index;
    }

    public void AttackingDone()
    {
        isAttacking = false;
    }


    public void DefendingDone()
    {
        isDefending = false;
        ChangeToAttack();
    }

    public void CounterDone()
    {
        ChangeToAttack();
    }


    void DecideWhatToDo()
    {
        if (health.IsHurt) return;

        reactiontime += Time.fixedDeltaTime;

        if (PlayerCombat.AttackType.None == player.CurrentAttack)
        {
            isPlayerAttacking = false;
        }

        else
        {
            isPlayerAttacking = true;
        }


        if (!wasPlayerAttacking && isPlayerAttacking && !isDefending && hasDefendParam)
        {
            ChangeToDefense();
            // player JUST started attacking
        }


        if (wasPlayerAttacking && !isPlayerAttacking && !isAttacking)
        {
            ChangeToCounter();
            // player JUST stopped attacking
        }

        wasPlayerAttacking = isPlayerAttacking;


    }
}