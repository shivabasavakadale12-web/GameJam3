using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    PlayerCombat player;
    Node rootNode;
    BoxCollider2D[] HitBox;
    float coolDown;
    Animator animator;
    BossEnemyAi bossEnemy;
    bool defendDone = false;
     void Start()
     {
        coolDown = 0f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        bossEnemy = GetComponent<BossEnemyAi>();
        HitBox = GetComponentsInChildren<BoxCollider2D>();

        foreach (var hitbox in HitBox)
        {
            hitbox.enabled = false;
        }

        animator = GetComponent<Animator>();

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
    
        Node attackBranch = new SequenceNode(new List<Node> {
                            new ConditionNode(() => bossEnemy.InRange), action1});

        Node DefendBranch = new SequenceNode(new List<Node>
        {
            new ConditionNode(() => player.IsAttacking && bossEnemy.InRange), defend
        });

        List<Node> children = new List<Node> { DefendBranch,  attackBranch, action2 };

        rootNode = new SelectorNode(children);
     }

     void Update()
     {
        rootNode.Evaluate();
     }


    public void DefendDone()
    {
        defendDone = false;
    }

}
