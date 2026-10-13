using UnityEngine;

[CreateAssetMenu(fileName = "New Character Roster", menuName = "Character Roster")]
public class CharacterRoster : ScriptableObject
{
    public CharacterData[] characters;
}
