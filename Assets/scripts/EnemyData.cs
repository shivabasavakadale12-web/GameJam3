using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int health;
    public float moveSpeed;
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