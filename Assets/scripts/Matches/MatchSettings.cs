using UnityEngine;

public static class MatchSettings
{
    public static CharacterData cd;
    public static void clear()
    {
       cd = null;
    }
    public static bool isValid()
    {
        return cd != null;
    }
}