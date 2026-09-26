using System.Collections.Generic;

public class SelectorNode : Node
{
    List <Node> children;

    public override NodeState Evaluate()
    {
        foreach (var child in children)
        {
            NodeState result = child.Evaluate();

            if (result == NodeState.Success || result == NodeState.Running)
            {
                return result;
            }
        }

        return NodeState.Failure;
    }

    public SelectorNode(List<Node> children)
    {
        this.children = children;
    }
}
