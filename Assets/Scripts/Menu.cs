using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Menu : MonoBehaviour
{
    private GameData _gameData;

    [SerializeField] private Text _health;
    [SerializeField] private Text _money;
    [SerializeField] private Text _difficulty;

    [SerializeField] private Image _playerSprite;


    [Inject]
    private void Construct(GameData gameData)
    {
        _gameData = gameData;
    }

    private void Start()
    {
        _playerSprite.sprite = DataController.CurrentCharacter.Sprite;

        _health.text = _gameData.MaxHealth.ToString();
        _money.text = _gameData.Food.ToString();
        _difficulty.text = "СЛОЖНОСТЬ: " + (_gameData.MaxHealth - 9);
    }
}
