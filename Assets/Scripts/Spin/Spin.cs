using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

public class Spin : MonoBehaviour
{
    private GameData _gameData;
    /*[SerializeField] private GameObject _prizePanel;
    [SerializeField] private Image _prizeSprite;
    [SerializeField] private Text _foodText;
    [SerializeField] private Text _lvlAbilityText;*/

    [field:SerializeField] public int Price { get; private set; }
    /*[SerializeField] private Text _priceText;*/

    private CharacterInfo[] _characters;

    [SerializeField] private SpinUI _spinUI;
    public static Action OnSpin;

    [Inject]
    private void Construct(GameData gameData)
    {
        _gameData = gameData;
    }
    private void Start()
    {
        _characters = Resources.Load<PlayerConfig>("Configs/PlayerConfig").Characters;
        /*_foodText.text = _gameData.Food.ToString();
        _priceText.text = _price.ToString();*/
    }
    
    public void BuySpin()
    {
        if (_gameData.Food < Price) 
            return;
        
        DoSpin();
        _gameData.Food -= Price;
        //_foodText.text = _gameData.Food.ToString();
    }
    private void DoSpin()
    {
        var prize = _gameData.CharacterData.ElementAt(Random.Range(0, _gameData.CharacterData.Count));
        prize.LvlAbility++;
        
        var prizeSprite = _characters.First(character => character.Id == prize.Id).Sprite;
        _spinUI.ShowPrize(prize.LvlAbility, prizeSprite);
        /*_prizePanel.SetActive(true);

        _lvlAbilityText.text = $"Ур. {prize.LvlAbility - 1} >> {prize.LvlAbility}";
        _prizeSprite.sprite = _characters.First(character => character.Id == prize.Id).Sprite;*/
        
        OnSpin?.Invoke();
    }
    /*public void GetPrize()
    {
        _prizePanel.SetActive(false);
    }*/
}
