using UnityEngine;

public class AdvancedEnemyHitBox : MonoBehaviour
{
    AdvancedEnemyAI enemyai;

    void Start()
    {
        enemyai = GetComponent<AdvancedEnemyAI>();
    }

    public void EnableCurrentHitbox()
    {
        enemyai.hitbox[enemyai.CurrentHitboxIndex].enabled = true;
    }

    public void DisableCurrentHitbox()
    {
        enemyai.hitbox[enemyai.CurrentHitboxIndex].enabled = false;
    }
}