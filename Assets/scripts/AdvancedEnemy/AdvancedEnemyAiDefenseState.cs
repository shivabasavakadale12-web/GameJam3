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

    }

    public override void Update()
    {
        int randomRoll = Random.Range(0, 100);

        if (randomRoll < enemy.Enemydata.defenseTendency && !enemy.isDefending && !enemy.Health.IsHurt)
        {
          enemy.isDefending = true;
          enemy.Animator.SetTrigger("defend");
        }

    }

    public override void Exit()
    {

    }
}
