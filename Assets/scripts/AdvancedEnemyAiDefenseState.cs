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
        enemy.Animator.SetTrigger("defend");
        Debug.Log("nigga defending stated nigga");
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("its done bitch");
    }
}
