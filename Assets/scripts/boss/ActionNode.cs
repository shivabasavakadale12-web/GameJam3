using System;
public class ActionNode: Node
{

    Func<NodeState> action;

    public override NodeState Evaluate()
    {

        return action();
    }

    public ActionNode(Func<NodeState> action)
    {
        this.action = action;
    }
}
