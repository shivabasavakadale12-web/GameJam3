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
        Debug.Log("Nigga Nigga Nigga Nigga!!!! HeHe nigga...");
    }

    public override void Update()
    {
            
    }

    public override void Exit()
    {
        Debug.Log("Thank you attacks done les go for defense");
    }
}
