using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Save : MonoBehaviour
{
    private string _saveFile;
    private FileStream _dataStream;
    private BinaryFormatter _converter = new();
    private GameData _gameData;

    public int Food;
    public int Health;
    public int HighScore;
    public string CurrentCharacterId;
    public List<CharacterData> CharacterData;

    [SerializeField] public CharacterInfo[] Characters;
    public CharacterInfo CurrentCharacter;
    private void Awake()
    {
        _saveFile = Application.persistentDataPath + "/gamedata.data";
        _gameData = new GameData();

        LoadData();
    }

    void LoadData()
    {
        if (File.Exists(_saveFile))
        {
            _dataStream = new FileStream(_saveFile, FileMode.Open);
            _gameData = _converter.Deserialize(_dataStream) as GameData;
            _dataStream.Close();
            GetData();
        }
    }

    void SaveData()
    {
        SetData();
        _dataStream = new FileStream(_saveFile, FileMode.Create);
        _converter.Serialize(_dataStream, _gameData);
        _dataStream.Close();
    }

    void SetData()
    {
        _gameData.Health = Health;
        _gameData.Money = Food;
        _gameData.HighScore = HighScore;
        _gameData.CurrentCharacterId = CurrentCharacterId;
        _gameData.CharacterData = CharacterData;
    }
    void GetData()
    {
        Health = _gameData.Health;
        Food = _gameData.Money;
        HighScore = _gameData.HighScore;
        CurrentCharacterId = _gameData.CurrentCharacterId;
        CharacterData = _gameData.CharacterData;

        DefaultValues();
    }
    void DefaultValues()
    {
        if (_gameData.Health == 0)
            Health = 10;

        if (_gameData.CharacterData == null)
            CharacterData = new();

        DefaultCharacterData();
    }
    void DefaultCharacterData()
    {
        //CharacterData = new();
        foreach (CharacterInfo character in Characters)
        {
            if (!CharacterData.Contains(CharacterData.Where(characterData => characterData.Id == character.Id).FirstOrDefault()))
            {
                CharacterData characterData = new(character.Id, 0);
                if (characterData.Id == "default")
                {
                    characterData.LvlAbility = 1;
                    CurrentCharacterId = characterData.Id;
                }

                CharacterData.Add(characterData);
            }
        }
        CurrentCharacter = Characters.Where(character => character.Id == CurrentCharacterId).First();
    }
    void OnApplicationPause()
    {
        SaveData();
    }
    void OnApplicationFocus(bool focus)
    {
        SaveData();
    }
}
