using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Text foodText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Image playerImage;

    private GameData _gameData;
    private int _highScore;

    [Inject]
    private void Construct(GameData gameData)
    {
        _gameData = gameData;
        _highScore = _gameData.HighScore;
    }

    private void Start()
    {
        _gameData.Food += Player.Food;
        _highScore = _highScore > Player.Food ? _highScore : Player.Food;
        _gameData.HighScore = _highScore;
        
        highScoreText.text = _highScore.ToString();
        foodText.text = Player.Food.ToString();
        playerImage.sprite = DataController.CurrentCharacter.Sprite; 
    }
}
