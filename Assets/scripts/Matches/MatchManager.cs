using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour
{
    [SerializeField] Transform EnemySpawn; 
    [SerializeField]  CharacterData defaultEnemy;
    [SerializeField] PlayerCombat player;
    [SerializeField] PlayerMovement Pm;
    [SerializeField] playerHealth playerHealth;
    [SerializeField] GameObject lostpanel;
    [SerializeField] GameObject WonPanel;
    IEnemy ai;
    IhealthEnemy aiHealth;
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
        aiHealth = enemy.GetComponent<IhealthEnemy>();
        if(ai == null)
        {
            Debug.LogWarning("this enemy has no Ienemy " + chosen.name );
            return;
        }
        player.SetEnemy(ai);
        playerHealth.onDead += PlayerLost;
        aiHealth.EnemyOnDead += PlayerWon;
    }


    void PlayerLost()
    {
        Debug.Log("you lost");
        ai?.StopFighting();
        lostpanel.SetActive(true);
    }

    void PlayerWon()
    {
        Debug.Log("you won");
        player.StopAction();
        Pm.enabled = false;
        WonPanel.SetActive(true);
    }

    public void home()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(3);
    }
    
}
