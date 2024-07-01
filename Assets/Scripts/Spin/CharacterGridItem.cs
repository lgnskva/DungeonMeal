using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class CharacterGridItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _lockMask;
    [SerializeField] private Image _sprite;

    private string _id;
    private int _lvlAbility;

    private CharacterChoose _characterChoose;
    private GameData _gameData;

    [Inject]
    private void Construct(CharacterChoose characterChoose, GameData gameData)
    {
        _characterChoose = characterChoose;
        _gameData = gameData;
    }
    
    public void Create(string id, Sprite sprite)
    {
        _id = id;
        _sprite.sprite = sprite;
        
        LockOrUnlock();
    }

    private void OnEnable()
    {
        Spin.OnSpin += LockOrUnlock;
    }

    private void OnDisable()
    {
        Spin.OnSpin -= LockOrUnlock;
    }

    private void LockOrUnlock()
    { 
        _lvlAbility = _gameData.CharacterData.First(character => character.Id == _id).LvlAbility;
        _lockMask.SetActive(_lvlAbility == 0);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        _characterChoose.OpenCharacter(_id);
    }
}
