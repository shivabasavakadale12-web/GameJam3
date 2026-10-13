using UnityEngine;
using UnityEngine.SceneManagement;


public class Switch : MonoBehaviour
{
    public void SwitchToQuickMatch()
    {
        SceneManager.LoadScene("EnemySelection");
    }

    public void SwitchToTournament()
    {
        SceneManager.LoadScene("TournamentScene");
    }
}
