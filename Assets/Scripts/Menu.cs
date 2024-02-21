using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Menu : MonoBehaviour
{
    private Save _save;

    [SerializeField] private Text _health;
    [SerializeField] private Text _money;
    [SerializeField] private Text _difficulty;

    [SerializeField] private Image _playerSprite;


    [Inject]
    void Construct(Save save)
    {
        _save = save;
    }
    private void Awake()
    {
    }
    void Start()
    {
        _playerSprite.sprite = _save.CurrentCharacter.Sprite;

        _health.text = _save.Health.ToString();
        _money.text = _save.Food.ToString();
        _difficulty.text = "СЛОЖНОСТЬ: " + (_save.Health - 9).ToString();
    }
}
