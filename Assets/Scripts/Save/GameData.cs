using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int Money;
    public int Health;
    public int HighScore;
    public string CurrentCharacterId;
    public List<CharacterData> CharacterData;
}
