using UnityEngine;

public class AdvabcedEnemyAttackStatee : AdvancedEnemyState
{
    AdvancedEnemyAI enemy;
    float attacktime;

    public AdvabcedEnemyAttackStatee(AdvancedEnemyAI enemy)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        attacktime = 0f;

    }

    public override void Update()
    {
        attacktime += Time.deltaTime;

        if (attacktime >= enemy.attackfrequency)
        {
            int randomindex = Random.Range(0, enemy.Enemydata.enemyattackdata.Length);
            EnemyAttackData attack = enemy.Enemydata.enemyattackdata[randomindex];
            enemy.Animator.SetTrigger(enemy.Enemydata.enemyattackdata[randomindex].animationTrigger);
            enemy.SetCurrentHitbox(attack.HitboxIndex);
            attacktime = 0f;
            Debug.Log(randomindex);
        }

        Debug.Log("Nigga Nigga Nigga Nigga!!!! HeHe nigga... ");
    }

    public override void Exit()
    {

        Debug.Log("nigga attacks done move to something else nigga!");
    }
}
