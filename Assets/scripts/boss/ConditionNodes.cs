using System;
public class ConditionNode: Node
{
    Func<bool> condition;

    public override NodeState Evaluate()
    {
        if(condition())
        {
            return NodeState.Success;
        }

        else
        {
            return NodeState.Failure;
        }
    }

    public ConditionNode(Func<bool> condition)
    {
        this.condition = condition;
    }
}
