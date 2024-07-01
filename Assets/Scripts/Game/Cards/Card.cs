using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

public abstract class Card : MonoBehaviour
{
    [SerializeField] private Text _damageText;
    
    public int Damage { get; private set; }

    public int X { get; private set; }
    public int Y { get; private set; }

    private GameData _gameData;
    protected Player Player;

    private CardAnimation _animation;

    public bool IsPoisoned;

    [Inject]
    public void Construct(GameData gameData, Player player)
    {
        _gameData = gameData;
        Player = player;
    }

    private void Start()
    {
        SetStartDamage();
        _animation = GetComponent<CardAnimation>();
    }

    protected virtual void SetStartDamage()
    {
        var difficulty = (MoveController.CountStep / 10) + _gameData.MaxHealth - 10;
        ChangeDamage(Random.Range(1, 6) + difficulty);
    }

    protected virtual void OnEnable()
    {
        MoveController.OnStep += Poison;
    }

    protected virtual void OnDisable()
    {
        MoveController.OnStep -= Poison;
    }

    public abstract void PerformAction();

    public async void DecreaseDamage(int damage)
    {
        Damage -= damage;

        if (Damage < 0)
            Damage = 0;
        
        _damageText.text = Damage.ToString();
        
        if (Damage == 0 && !MoveController.IsMove)
        {
            await _animation.Death();
        }
    }

    public void ChangeDamage(int damage)
    {
        if (damage <= 0)
            return;
        Damage = damage;
        _damageText.text = Damage.ToString();
    }

    public void SetCoordinates(int x = -1, int y = -1)
    {
        if (x != -1)
            X = x;
        if (y != -1)
            Y = y;
    }

    private void Poison()
    {
        if (!IsPoisoned || Damage <= 1)
        {
            IsPoisoned = false;
            return;
        }
        DecreaseDamage(1);
    }
}
