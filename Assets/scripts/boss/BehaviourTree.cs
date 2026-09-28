using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    public EnemyData enemyData;
    Health Health;
    PlayerCombat player;
    Node rootNode;
    BoxCollider2D[] HitBox;
    float coolDown;
    Animator animator;
    BossEnemyAi bossEnemy;
    Rigidbody2D rb;
    bool defendDone = false;
    bool hurtDone = false;
    bool deadDone = false;
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
             animator.SetTrigger("Death");
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
            animator.SetTrigger("defend");
            Debug.Log("Defending player attack");
            defendDone = true;
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
              coolDown = 0f;
            }
            return NodeState.Success;
        });

        ActionNode action2 = new ActionNode(() =>
        {
            Debug.Log("action2 is running");
            return NodeState.Success;
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
                            new ConditionNode(() => bossEnemy.InRange), action1});

        Node DefendBranch = new SequenceNode(new List<Node>
        {
            new ConditionNode(() => player.IsAttacking && bossEnemy.InRange && !Health.isHurt), defend
        });

        List<Node> children = new List<Node> { DeathBranch, HurtBranch, DefendBranch,  attackBranch, action2 };

        rootNode = new SelectorNode(children);
     }

     void Update()
     {
        rootNode.Evaluate();
     }

    void HurtDone()
    {
        Health.isHurt = false;
        hurtDone = false;
    }


    public void DefendDone()
    {
        defendDone = false;
    }

    void DeadDone()
    {
        Destroy(gameObject);
    }

}
