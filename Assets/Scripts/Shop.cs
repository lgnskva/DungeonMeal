using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Shop : MonoBehaviour
{
    [SerializeField] private Text _foodText;
    [SerializeField] private Text _priceHealthText;

    private GameData _gameData;

    private int _priceHealth;

    [Inject]
    void Construct(GameData gameData)
    {
        _gameData = gameData;
    }
    void Start()
    {
        _priceHealth = _gameData.MaxHealth * 100;

        _foodText.text = _gameData.Food.ToString();
        _priceHealthText.text = _priceHealth.ToString();
    }
    public void BuyHealth()
    {
        Buy(ref _priceHealth, ref _gameData.MaxHealth);
        _priceHealth = _gameData.MaxHealth * 100;
        _priceHealthText.text = _priceHealth.ToString();
    }
    void Buy(ref int price, ref int stat)
    {
        if (_gameData.Food > price)
        {
            stat++;
            _gameData.Food -= price;
            _foodText.text = _gameData.Food.ToString();
        }
    }
}
