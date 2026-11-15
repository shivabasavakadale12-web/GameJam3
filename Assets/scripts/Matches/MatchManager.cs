using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [SerializeField] Transform EnemySpawn; 
    [SerializeField]  CharacterData defaultEnemy;

    CharacterData chosen;

    void Start()
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
    }
}
