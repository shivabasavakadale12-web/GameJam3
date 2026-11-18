using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [SerializeField] Transform EnemySpawn; 
    [SerializeField]  CharacterData defaultEnemy;
    [SerializeField] PlayerCombat player;
    [SerializeField] playerHealth playerHealth;
    IEnemy ai;
    CharacterData chosen;

    void Awake()
    {
        if(MatchSettings.isValid())
        {
            chosen = MatchSettings.cd;
        }

        else
        {
            Debug.LogWarning("Enemy is default not the chosen one");
            chosen = defaultEnemy;
        }

        GameObject enemy = Instantiate(chosen.Character, EnemySpawn.position, Quaternion.identity);
        ai = enemy.GetComponent<IEnemy>();

        if(ai == null)
        {
            Debug.LogWarning("this enemy has no Ienemy " + chosen.name );
            return;
        }
        player.SetEnemy(ai);
        playerHealth.onDead += PlayerLost;
    }


    void PlayerLost()
    {
        Debug.Log("you lost");
        ai?.StopFighting();
    }
}
