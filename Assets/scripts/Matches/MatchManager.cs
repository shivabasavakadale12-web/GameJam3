using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [SerializeField] Transform EnemySpawn; 
    [SerializeField]  CharacterData defaultEnemy;
    [SerializeField] PlayerCombat player;
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
        IEnemy ai = enemy.GetComponent<IEnemy>();

        if(ai == null)
        {
            Debug.LogWarning("this enemy has no Ienemy " + chosen.name );
            return;
        }
        player.SetEnemy(ai);
    }
}
