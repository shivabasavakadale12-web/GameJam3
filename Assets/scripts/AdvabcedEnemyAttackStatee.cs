using UnityEngine;

public class AdvabcedEnemyAttackStatee : AdvancedEnemyState
{
    AdvancedEnemyAI enemy;

    public AdvabcedEnemyAttackStatee(AdvancedEnemyAI enemy)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        Debug.Log("Enemy entered attack range");
    }

    public override void Update()
    {
            
    }

    public override void Exit()
    {
        
    }
}
