using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int health;
    public int attack1;
    public int attack2;
    public int attack3;
    public int counterattackdamage;
    public int playerattack1;
    public int playerpowerattack;
    public int playersuperpowerattack;
    public float moveSpeed;
    public float rundistance;
    public float walkdistance;
    public int attackDamage;
    public float attackFrequency;
    public float attackRange;
    public float defenseTendency;
    public float reactionTime;
    public float dodgeTendency;
    public float superAttackTendency;
    public float aggression;
    public float counterAttackTendency;
    public bool HasSuperPower;
    
}