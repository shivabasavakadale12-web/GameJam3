using System.Collections;
using System.Collections.Generic;
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
    public bool isDefending = false;
    bool DefendingStarted = false;
    bool defendrollPassed = false;
    bool isHurt = false;
    public bool deadDone = false;
    bool HasCheckedCurrentSwing = false;
    bool isSuperAggressive = false;
    bool powerAttackStarted = false;
    bool powerAttackRollPassed = false;
    int counterattack = 0;
    float WindowTimer = 0f;
    int counterattackIndex = 0;

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
            if (!isHurt)
            {
                animator.SetTrigger("hurt");
                HasCheckedCurrentSwing = true;
                isHurt = true;
            }
                return NodeState.Success;
        });

        ActionNode defend = new ActionNode(() =>
        {
           if(!HasCheckedCurrentSwing && !isDefending && !DefendingStarted)
           {
                HasCheckedCurrentSwing = true;

                int randomRoll = Random.Range(0, 100);

                if(randomRoll <= enemyData.defenseTendency)
                {
                 defendrollPassed = true;
                 DefendingStarted = true;
                 StartCoroutine(DefendWithReaction());
                }
           }
            return defendrollPassed ? NodeState.Success : NodeState.Failure;
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
                int randomRoll = Random.Range(0, 100);
            
                if(randomRoll <= enemyData.counterAttackTendency)
                {
                  powerAttackRollPassed = true;
                  powerAttackStarted = true;
                  StartCoroutine(Powerattacck());
                }

                else
                {
                    powerAttackRollPassed = false;
                    powerAttackStarted = true;
                    StartCoroutine(DefaultPowerAttack());
                }

            }
            return powerAttackRollPassed ? NodeState.Success : NodeState.Failure;
        });

        Node PowerAttackBranch = new SequenceNode(new List<Node>
        {
            new ConditionNode(() => bossEnemy.InRange && isSuperAggressive
            && !isDefending && !Health.isHurt), powerAttack
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
            new ConditionNode(() => player.IsAttacking && bossEnemy.InRange && !Health.isHurt && !isDefending), defend
        });

        List<Node> children = new List<Node> { DeathBranch, HurtBranch, DefendBranch, attackBranch, PowerAttackBranch};

        rootNode = new SelectorNode(children);
     }

     void Update()
     {
        rootNode.Evaluate();
        WindowTimer += Time.deltaTime;

        if(WindowTimer >= 14f)
        {
            isSuperAggressive = counterattack >= 3;
            WindowTimer = 0f;
            counterattack = 0;
        }


        if(!player.IsAttacking)
        {
            HasCheckedCurrentSwing = false;
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
                counterattackIndex += 1;
                break;
            case PlayerCombat.AttackType.Attack2:
                counterattackIndex += 1;
                break;

            case PlayerCombat.AttackType.PowerAttack:
                counterattackIndex += 2;
                break;
            case PlayerCombat.AttackType.SuperPowerAttack:
                counterattackIndex += 3;
                break;
        }

    }

    void HurtDone()
    {
        Health.isHurt = false;
        isHurt = false;
    }


    public void Defending()
    {
        DefendingStarted = false;
        isDefending = false;
    }

    void DeadDone()
    {
        Destroy(gameObject);
    }

    IEnumerator DefendWithReaction()
    {
        yield return new WaitForSeconds(enemyData.reactionTime);

        if (isHurt || Health.isDead)
        {
            DefendingStarted = false;
            yield break;
        }
     
        animator.SetTrigger("defend");
        isDefending = true;
    }

    IEnumerator Powerattacck()
    {
        yield return new WaitForSeconds(enemyData.reactionTime);

        if( counterattackIndex >= 3)
        {
            enemyData.counterAttackTendency = enemyData.counterAttackTendency + enemyData.SwapnaGoodGirl;
        }
            animator.SetTrigger("Attack4");
            coolDown = 0f;
            powerAttackStarted = false;
    }

    IEnumerator DefaultPowerAttack()
    {
        yield return new WaitForSeconds(enemyData.reactionTime);
        animator.SetTrigger("Attack3");
        coolDown = 0f;
        powerAttackStarted = false;
    }

}
