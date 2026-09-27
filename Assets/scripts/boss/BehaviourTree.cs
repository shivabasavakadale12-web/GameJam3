using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    Node rootNode;

     void Start()
     {
        ActionNode action1 = new ActionNode(() =>
        {
            Debug.Log("action1 is running");
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
