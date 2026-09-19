using UnityEngine;

public class AdvancedEnemyAiCounterAttackState : AdvancedEnemyState
{
    AdvancedEnemyAI enemy;
    float AttackFrequency;

    public AdvancedEnemyAiCounterAttackState(AdvancedEnemyAI enemy)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        enemy.moveSpeed += 1.7f;
        enemy.attackfrequency = 0.4f;
        AttackFrequency = 0f;
    }

    public override void Update()
    {
        AttackFrequency += Time.fixedDeltaTime;

        if(AttackFrequency >= enemy.attackfrequency)
        {
         int randomIndex = Random.Range(enemy.Enemydata.enemyattackdata.Length - 2,
                                        enemy.Enemydata.enemyattackdata.Length);


         EnemyAttackData attack = enemy.Enemydata.enemyattackdata[randomIndex];
         string trigger = enemy.Enemydata.enemyattackdata[randomIndex].animationTrigger;
         enemy.SetCurrentHitbox(attack.HitboxIndex);
         enemy.Animator.SetTrigger(trigger);
         AttackFrequency = 0f;
        }


    }

    public override void Exit()
    {
        enemy.attackfrequency = enemy.Enemydata.attackFrequency;
        enemy.moveSpeed = enemy.Enemydata.moveSpeed;
    }
}
