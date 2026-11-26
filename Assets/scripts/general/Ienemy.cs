using System;
using UnityEngine;

public interface IEnemy
{
    EnemyData Enemydata { get; }
    bool IsAttacking { get; }
    void StopFighting();
    event Action CounterAttack;
    Transform EnemyTransform { get; }
    bool IsStrikeActive { get; }
}
