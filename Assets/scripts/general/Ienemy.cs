using UnityEngine;

public interface IEnemy
{
    EnemyData Enemydata { get; }
    bool IsAttacking { get; }
    void StopFighting();
    bool IsStrikeActive { get; }
}
