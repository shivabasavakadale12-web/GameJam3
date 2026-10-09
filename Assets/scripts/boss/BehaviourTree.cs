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
    float powerAttackCoolDown;
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
    bool lockaction = false;
    int counterattack = 0;
    float WindowTimer = 0f;
    float counterTendencyBonus = 0f;
    int counterattackIndex = 0;

    public bool isAttacking = false;

    public int CurrentHitboxIndex { get; set; }

    public EnemyData Enemydata => enemyData;

    public void SetColliderOff()
    {
        foreach (var hitbox in HitBox)
        {
            hitbox.enabled = false;
        }
    }

    public void SetCurrentHitbox(int index)
    {
        CurrentHitboxIndex = index;
    }
    public void LockAction(bool value)
    {
        lockaction = value;
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
            if(!deadDone && !lockaction)
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
            if (!isHurt && !lockaction)
            {
                animator.SetTrigger("hurt");
                HasCheckedCurrentSwing = true;
                isHurt = true;
                isDefending = false;
                DefendingStarted = false;
            }
            return NodeState.Success;
        });

        ActionNode defend = new ActionNode(() =>
        {
           if(!HasCheckedCurrentSwing && !isDefending && !DefendingStarted && !lockaction)
           {
                HasCheckedCurrentSwing = true;

                int randomRoll = Random.Range(0, 100);
                Debug.Log("ROll: " + randomRoll + " Vs Defense Tendency: " + enemyData.defenseTendency);

                if (randomRoll <= enemyData.defenseTendency)
                {
                    defendrollPassed = true;
                    DefendingStarted = true;

                    if (player.CurrentAttack == PlayerCombat.AttackType.SuperPowerAttack || player.CurrentAttack == PlayerCombat.AttackType.PowerAttack)
                    {
                        StartCoroutine(SuperDefending());
                    }
                    else
                    {
                        
                        StartCoroutine(DefendWithReaction());
                    }
                }
           }
            return defendrollPassed ? NodeState.Success : NodeState.Failure;
        }   
        );

        ActionNode action1 = new ActionNode(() =>
        {
            coolDown += Time.deltaTime;

            if (coolDown >= enemyData.attackFrequency && !lockaction)
            {
              int randomRoll = Random.Range(0, 100);



                if(randomRoll <= 50)
                {               
                 animator.SetTrigger("Attack4");
                 Debug.Log("action1 is running");
                 isAttacking = true;
                 coolDown = 0f;
                }

                else
                {
                    animator.SetTrigger("Attack2");
                    Debug.Log("action2 is running");
                    isAttacking = true;
                    coolDown = 0f;
                }
            }
            return NodeState.Success;
        });

        ActionNode powerAttack = new ActionNode(() =>
        {


            powerAttackCoolDown += Time.deltaTime;
            if (powerAttackCoolDown >= enemyData.attackFrequency && !powerAttackStarted && !lockaction)
            {
                powerAttackRollPassed = false;
                int randomRoll = Random.Range(0, 100);
                float effectiveTendency = Mathf.Clamp(enemyData.counterAttackTendency + counterTendencyBonus, 0, 100);

                if (randomRoll <= effectiveTendency)
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
                            new ConditionNode(() => bossEnemy.InRange && !isAttacking), action1});


        Node DefendBranch = new SequenceNode(new List<Node>
        {
            new ConditionNode(() => player.IsAttacking && bossEnemy.InRange && !Health.isHurt && !isDefending), defend
        });

        List<Node> children = new List<Node> { DeathBranch, HurtBranch, DefendBranch, PowerAttackBranch, attackBranch };

        rootNode = new SelectorNode(children);
     }

     void Update()
     {
        if(lockaction)
        {
            return;
        }
        rootNode.Evaluate();
        WindowTimer += Time.deltaTime;

        if(WindowTimer >= 14f)
        {
            isSuperAggressive = counterattack >= 3;
            Debug.Log("Aggression window closed — counterattack: " + counterattack + " isSuperAggressive: " + isSuperAggressive);
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
        Debug.Log("Defending() reset called at " + Time.time);
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

        yield return new WaitForSeconds(0.867f);

        isDefending = false;
        DefendingStarted = false;
    }

    IEnumerator Powerattacck()
    {
        yield return new WaitForSeconds(enemyData.SuperReactionTime);

        if (counterattackIndex >= 3)
        {
            counterTendencyBonus = Mathf.Min(counterTendencyBonus + enemyData.ExtraTendency, 50f); 
        }

        animator.SetTrigger("Attack4");
        powerAttackCoolDown = 0f;
        powerAttackStarted = false;
        counterattackIndex = 0;
    }

    IEnumerator SuperDefending()
    {
        yield return new WaitForSeconds(enemyData.SuperReactionTime);
        if (isHurt || Health.isDead)
        {
            DefendingStarted = false;
            yield break;
        }
        animator.SetTrigger("defend");
        isDefending = true;
        yield return new WaitForSeconds(0.867f);
        isDefending = false;
        DefendingStarted = false;
    }
    IEnumerator DefaultPowerAttack()
    {
        yield return new WaitForSeconds(enemyData.reactionTime);
        animator.SetTrigger("Attack3");
        powerAttackCoolDown = 0f;
        powerAttackStarted = false;
    }

}
