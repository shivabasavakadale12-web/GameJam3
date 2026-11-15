using UnityEngine;

public interface IEnemy
{
    EnemyData Enemydata { get; }
    bool IsAttacking { get; }
    bool IsStrikeActive { get; }
}
