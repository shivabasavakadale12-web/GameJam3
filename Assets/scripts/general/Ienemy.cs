using UnityEngine;

public interface IEnemy
{
    EnemyData Enemydata { get; }
    bool IsAttacking { get; }
    void StopFighting();
    Transform EnemyTransform { get; }
    bool IsStrikeActive { get; }
}
