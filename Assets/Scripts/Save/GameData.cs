using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class GameData
{
    public int Food;
    public int MaxHealth;
    public int HighScore;
    public string CurrentCharacterId;
    public List<CharacterData> CharacterData;
}
