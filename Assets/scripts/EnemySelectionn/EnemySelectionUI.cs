using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemySelectUI : MonoBehaviour
{
    [SerializeField] CharacterRoster roster;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform gridParent;      
    [SerializeField] Button startButton;
    [SerializeField] Image previewImage;
    [SerializeField] TMP_Text previewName;

    const string fightScene = "Fight arena";
    const string menuScene = "Main Menu";

    void Start()
    {
        // 1. clear MatchSettings
        // 2. disable the Start button
        // 3. loop over roster.characters
        //    - Instantiate buttonPrefab with gridParent as the parent
        //    - set its name text from the data
        //    - add a click listener that calls SelectEnemy with THIS entry
    }

    void SelectEnemy(CharacterData data)
    {
        // 1. store data in MatchSettings
        // 2. update previewImage and previewName
        // 3. enable the Start button
    }

    public void OnStartPressed()
    {
        SceneManager.LoadScene(fightScene);
    }

    public void OnBackPressed()
    {
        SceneManager.LoadScene(menuScene);
    }
}