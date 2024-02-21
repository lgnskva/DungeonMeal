[System.Serializable]
public class CharacterData
{
    public string Id;
    public int LvlAbility;

    public CharacterData(string id, int lvlAbility)
    {
        Id = id;
        LvlAbility = lvlAbility;
    }
}
