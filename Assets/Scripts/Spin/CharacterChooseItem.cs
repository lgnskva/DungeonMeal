using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CharacterChooseItem : MonoBehaviour
{
    [SerializeField] private Image _characterSprite;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _lvlText;
    [SerializeField] private GameObject _button;

    public string Id { get; private set; }
    public int LvlAbility;

    private CharacterInfo _character;
    private GameData _gameData;

    [Inject]
    private void Construct(GameData gameData)
    {
        _gameData = gameData;
    }
    
    public void Create(CharacterInfo character)
    {
        _character = character;
        Id = _character.Id;
        _characterSprite.sprite = _character.Sprite;
        _descriptionText.text = _character.GetDescription();
        _nameText.text = _character.Name;

        UpdateLvl();
    }

    private void LockOrUnlock()
    {
        if (LvlAbility == 0)
        {
            _button.GetComponentInChildren<Text>().text = "НЕДОСТУПНО";
            _button.GetComponent<Button>().interactable = false;
        }
        else if (Id == _gameData.CurrentCharacterId)
        {
            _button.GetComponentInChildren<Text>().text = "ВЫБРАНО";
            _button.GetComponent<Button>().interactable = false;
        }
        else
        {
            _button.GetComponentInChildren<Text>().text = "ВЫБРАТЬ";
            _button.GetComponent<Button>().interactable = true;
        }
    }

    private void OnEnable()
    {
        UpdateLvl();
        LockOrUnlock();
        
        Spin.OnSpin += UpdateLvl;
    }

    private void OnDisable()
    {
        Spin.OnSpin -= UpdateLvl;
    }

    private void UpdateLvl()
    {
        _descriptionText.text = _character.GetDescription();
        LvlAbility = _gameData.CharacterData.First(character => character.Id == Id).LvlAbility;
        _lvlText.text = "Ур. " + LvlAbility;
        
        LockOrUnlock();
    }
    public void ChooseButton()
    {
        DataController.CurrentCharacter = _character;
        _gameData.CurrentCharacterId = Id;
        LockOrUnlock();
    }
}
