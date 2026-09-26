using System.Collections.Generic;


public class SequenceNode: Node
{

    List <Node> children;

    public override NodeState Evaluate()
    {
        foreach (var child in children)
        {
            NodeState result = child.Evaluate();

            if(result == NodeState.Failure || result == NodeState.Running)
            {
                return result;
            }

        }

        return NodeState.Success;
    }

    public SequenceNode(List<Node> children)
    {
        this.children = children;
    }
}
