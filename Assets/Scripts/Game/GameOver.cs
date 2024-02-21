using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Text foodText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Image playerImage;

    private Save _save;
    private Player _player;

    [Inject]
    void Construct(Save save, Player player)
    {
        _save = save;
        _player = player;
    }
    void Start()
    {
        _save.HighScore = _save.HighScore > _player.food ? _save.HighScore : _player.food;
        highScoreText.text = _save.HighScore.ToString();
        foodText.text = _player.food.ToString();
        playerImage.sprite = _save.CurrentCharacter.Sprite; 
    }
}
