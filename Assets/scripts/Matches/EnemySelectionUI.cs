using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EnemysselectUI : MonoBehaviour
{
    [SerializeField] CharacterRoster roster;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform gridParent;      
    [SerializeField] Button startButton;
    [SerializeField] Image previewImage;
    [SerializeField] TMP_Text previewName;

    const string fightScene = "FightArena";
    const string menuScene = "Main Menu";

    void Start()
    {
        MatchSettings.clear();
        startButton.interactable = false;

        foreach(CharacterData cd in roster.characters)
        {     
          GameObject btn = Instantiate(buttonPrefab, gridParent);
          TMP_Text label = btn.GetComponentInChildren<TMP_Text>();
          label.text = cd.characterName;
          Button button = btn.GetComponent<Button>();
          button.onClick.AddListener(() => SelectEnemy(cd));
        }
    }

    void SelectEnemy(CharacterData data)
    {
        MatchSettings.cd = data;
        Debug.Log(data.characterName);
        startButton.interactable = true;
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