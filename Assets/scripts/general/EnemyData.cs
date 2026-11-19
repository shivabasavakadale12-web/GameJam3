using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int health;
    public int attack1;
    public float attack1Range;
    public int attack2;
    public float attack2Range;
    public int attack3;
    public float attack3Range;
    public int counterattackdamage;
    public float counterattackRange;
    public int playerattack1;
    public int playerpowerattack;
    public int playersuperpowerattack;
    public float keepDistance;
    public float moveSpeed;
    public float runspeed;
    public float rundistance;
    public float walkdistance;
    public int attackDamage;
    public float attackFrequency;
    public float CounterAttackFrequency;
    public float CounterAttackMovespeed;
    public float attackRange;
    public float defenseTendency;
    public float reactionTime;
    public float dodgeTendency;
    public float superAttackTendency;
    public float aggression;
    public float counterAttackTendency;
    public bool HasSuperPower;
    public float SuperReactionTime;
    public float ExtraTendency;
    public EnemyAttackData[] enemyattackdata;

}