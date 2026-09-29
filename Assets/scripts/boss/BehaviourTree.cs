using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BehaviourTree : MonoBehaviour, IEnemy
{
    public BoxCollider2D[] HitBox;
    public EnemyData enemyData;
    Health Health;
    PlayerCombat player;
    Node rootNode;
    float coolDown;
    Animator animator;
    BossEnemyAi bossEnemy;
    Rigidbody2D rb;
    bool defendDone = false;
    bool hurtDone = false;
    bool deadDone = false;
    bool isSuperAggressive = false;
    bool powerAttackStarted = false;
    int isCounterAttackTrue = 100;
    int counterattack = 0;
    float WindowTimer = 0f;

    public bool isAttacking = false;

    public int CurrentHitboxIndex { get; set; }

    public EnemyData Enemydata => enemyData;

    public void SetCurrentHitbox(int index)
    {
        CurrentHitboxIndex = index;
    }

    void Start()
     {
        coolDown = 0f;
        rb = GetComponent<Rigidbody2D>();
        Health = GetComponent<Health>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        bossEnemy = GetComponent<BossEnemyAi>();
        HitBox = GetComponentsInChildren<BoxCollider2D>();

        foreach (var hitbox in HitBox)
        {
            hitbox.enabled = false;
        }

        animator = GetComponent<Animator>();

        ActionNode dead = new ActionNode(() =>

        {
            if(!deadDone)
            {
             rb.linearVelocity = Vector2.zero;
             bossEnemy.enabled = false;
             animator.SetTrigger("dead");
             Invoke("DeadDone", 3f);
             deadDone = true;
            }
            return NodeState.Success;
        });

        ActionNode hurt = new ActionNode(() =>

        {
            if (!hurtDone)
            {
                animator.SetTrigger("hurt");
                hurtDone = true;
            }
                return NodeState.Success;
        });

        ActionNode defend = new ActionNode(() =>
        {
           if(!defendDone)
           {
                StartCoroutine(DefendWithReaction());
           }
            return NodeState.Success;
        }
        
        );

        ActionNode action1 = new ActionNode(() =>
        {
            coolDown += Time.deltaTime;
            if(coolDown >= enemyData.attackFrequency)
            {
              animator.SetTrigger("Attack1");
              Debug.Log("action1 is running");
              isAttacking = true;
              coolDown = 0f;
            }
            return NodeState.Success;
        });

        ActionNode action2 = new ActionNode(() =>
        {
            coolDown += Time.deltaTime;
            if(coolDown >= enemyData.attackFrequency)
            {
             animator.SetTrigger("Attack2");
             Debug.Log("action2 is running");
             isAttacking = true;
             coolDown = 0f;       
            }
            return NodeState.Success;
        });

        ActionNode powerAttack = new ActionNode(() =>
        {
            coolDown += Time.deltaTime;
            if (coolDown >= enemyData.attackFrequency && !powerAttackStarted)
            {
                powerAttackStarted = true;
                StartCoroutine(Powerattacck());
            }

            return NodeState.Success;
        });

        Node PowerAttackBranch = new SequenceNode(new List<Node>
        {
            new ConditionNode(() => isSuperAggressive && !defendDone && !Health.isHurt), powerAttack
        });

        Node DeathBranch = new SequenceNode(new List<Node>
        {
            new ConditionNode(() => Health.isDead), dead
        });

        Node HurtBranch = new SequenceNode(new List<Node>
        {
            new ConditionNode(() => Health.isHurt), hurt
        });
    
        Node attackBranch = new SequenceNode(new List<Node> {
                            new ConditionNode(() => bossEnemy.InRange && !isAttacking), action1, action2});


        Node DefendBranch = new SequenceNode(new List<Node>
        {
            new ConditionNode(() => player.IsAttacking && bossEnemy.InRange && !Health.isHurt), defend
        });

        List<Node> children = new List<Node> { DeathBranch, HurtBranch, DefendBranch,  attackBranch, PowerAttackBranch};

        rootNode = new SelectorNode(children);
     }

     void Update()
     {
        rootNode.Evaluate();
        WindowTimer += Time.deltaTime;

        if(WindowTimer >= 14f)
        {
            isSuperAggressive = counterattack >= 4;
            WindowTimer = 0f;
            counterattack = 0;
        }

     }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("PlayerAttack"))
            return;

        counterattack++;

        switch (player.CurrentAttack)
        {
            case PlayerCombat.AttackType.Attack1:
                isCounterAttackTrue -= 2;
                break;
            case PlayerCombat.AttackType.Attack2:
                isCounterAttackTrue -= 4;
                break;
            case PlayerCombat.AttackType.PowerAttack:
                isCounterAttackTrue -= 6;
                break;
            case PlayerCombat.AttackType.SuperPowerAttack:
                isCounterAttackTrue -= 10;
                break;
        }

        isCounterAttackTrue = Mathf.Max(isCounterAttackTrue, 0);
    }

    void HurtDone()
    {
        Health.isHurt = false;
        hurtDone = false;
    }


    public void Defending()
    {
        defendDone = false;
    }

    void DeadDone()
    {
        Destroy(gameObject);
    }

    IEnumerator DefendWithReaction()
    {
        yield return new WaitForSeconds(enemyData.reactionTime);
        animator.SetTrigger("defend");
        Debug.Log("Defending player attack");
        defendDone = true;
    }

    IEnumerator Powerattacck()
    {
        yield return new WaitForSeconds(enemyData.reactionTime);

        if (isCounterAttackTrue <= enemyData.counterAttackTendency)
            animator.SetTrigger("Attack4");
        else
            animator.SetTrigger("attack3");

        coolDown = 0f;
        powerAttackStarted = false;
    }


}
