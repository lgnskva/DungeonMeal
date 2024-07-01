using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class DataController : MonoBehaviour
{
    private static GameData _gameData;

    private static PlayerConfig _playerConfig;

    public static CharacterInfo CurrentCharacter;
    
    public static void SetDefaultValues(GameData gameData)
    {
        _gameData = gameData;
        _playerConfig = Resources.Load<PlayerConfig>("Configs/PlayerConfig");
        
        if (_gameData.MaxHealth == 0)
            _gameData.MaxHealth = _playerConfig.MaxHealth;

        if (_gameData.CharacterData == null)
            _gameData.CharacterData = new List<CharacterData>();

        DefaultCharacterData();
    }
    private static void DefaultCharacterData()
    {
        //CharacterData = new();
        foreach (var character in _playerConfig.Characters)
        {
            var characterData = _gameData.CharacterData.FirstOrDefault(characterData => characterData.Id == character.Id);
            if (characterData == null)
            {
                characterData = new CharacterData(character.Id, 0);
                if (characterData.Id == _playerConfig.DefaultCharacterId)
                {
                    characterData.LvlAbility = 1;
                    _gameData.CurrentCharacterId = characterData.Id;
                }

                _gameData.CharacterData.Add(characterData);
            }
            character.SetLvlAbility(characterData.LvlAbility);
        }
        CurrentCharacter = _playerConfig.Characters.First(character => character.Id == _gameData.CurrentCharacterId);
    }
}
