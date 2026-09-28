using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    Node rootNode;
    BoxCollider2D[] HitBox;
    float coolDown;
    Animator animator;
     void Start()
     {
        coolDown = 0f;
        HitBox = GetComponentsInChildren<BoxCollider2D>();

        foreach (var hitbox in HitBox)
        {
            hitbox.enabled = false;
        }

        animator = GetComponent<Animator>();
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
    
        List<Node> children = new List<Node> { action1, action2};

        rootNode = new SelectorNode(children);
     }

     void Update()
    {
        rootNode.Evaluate();
    }

}
