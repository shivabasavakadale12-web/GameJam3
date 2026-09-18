using UnityEngine;

public class AdvancedEnemyAiDefenseState : AdvancedEnemyState
{
    AdvancedEnemyAI enemy;
    public AdvancedEnemyAiDefenseState(AdvancedEnemyAI enemy)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        enemy.isDefending = true;
        Debug.Log("DEFENSE STATE ENTERED");
        enemy.Animator.SetTrigger("defend");
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("its done bitch");
    }
}
