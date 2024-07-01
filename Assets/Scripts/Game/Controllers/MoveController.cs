using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class MoveController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Player _player;
    private CardFactory _cardFactory;
    
    public static bool IsMove;
    public static bool IsStop;
    public static bool IsAbilityWait;
    public static int CountStep { get; private set; }
    private GameObject[,] _cards;

    public static event Action OnStep;

    private static int _xSize;
    private static int _ySize;
    
    public static int PlayerX;
    public static int PlayerY;

    [Inject]
    private void Construct(Player player, CardFactory cardFactory)
    {
        _player = player;
        _cardFactory = cardFactory;
        
        SetDefaultValues();
    }

    private void SetDefaultValues()
    {
        CountStep = 0;
        
        IsMove = false;
        IsStop = false;
        IsAbilityWait = false;

        PlayerX = 1;
        PlayerY = 1;

        var config = Resources.Load<SpawnConfig>("Configs/SpawnConfig");
        _xSize = config.XSize;
        _ySize = config.YSize;

        _cards = CardFactory.Cards;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && PlayerX < _xSize-1)
            DoStep(_cards[PlayerX + 1, PlayerY], MoveRight);
        if (Input.GetKeyDown(KeyCode.A) && PlayerX > 0)
            DoStep(_cards[PlayerX - 1, PlayerY], MoveLeft);
        if (Input.GetKeyDown(KeyCode.W) && PlayerY < _ySize-1)
            DoStep(_cards[PlayerX, PlayerY + 1], MoveUp);
        if (Input.GetKeyDown(KeyCode.S) && PlayerY > 0)
            DoStep(_cards[PlayerX, PlayerY - 1], MoveDown);
    }
    public void OnEndDrag(PointerEventData data)
    {
        var dir = (data.position - data.pressPosition).normalized;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0 && PlayerX < _xSize-1)
                DoStep(_cards[PlayerX + 1, PlayerY], MoveRight);
            else if (PlayerX > 0)
                DoStep(_cards[PlayerX - 1, PlayerY], MoveLeft);
        }
        else
        {
            if (dir.y > 0 && PlayerY < _ySize-1)
                DoStep(_cards[PlayerX, PlayerY + 1], MoveUp);
            else if (PlayerY > 0)
                DoStep(_cards[PlayerX, PlayerY - 1], MoveDown);
        }
    }
    
    private static void DoStep(GameObject damagedCard, Action move)
    {
        if (IsMove)
            return;
        IsMove = true;
        damagedCard.GetComponent<Card>().PerformAction();
        CountStep++;
        OnStep?.Invoke();

        if (!IsStop)
            move();
        else IsMove = false;

        IsStop = false;
    }

    private async void MoveRight()
    {
        var eatenCard = _cards[PlayerX + 1, PlayerY].GetComponent<CardAnimation>();
        await eatenCard.Death();

        for (var i = PlayerX - 1; i >= 0; i--)
        {
            _cards[i, PlayerY].GetComponent<CardAnimation>().MoveTo(1, 0);
            _cards[i + 1, PlayerY] = _cards[i, PlayerY];
            _cards[i + 1, PlayerY].GetComponent<Card>().SetCoordinates(x: i + 1);
        }

        _player.GetComponent<CardAnimation>().MoveTo(1, 0);

        _cardFactory.CreateCard(0, PlayerY);
        PlayerX++;
        _cards[PlayerX, PlayerY] = _player.gameObject;
        IsMove = false;
    }

    private async void MoveLeft()
    {
        var eatenCard = _cards[PlayerX - 1, PlayerY].GetComponent<CardAnimation>();
        await eatenCard.Death();

        for (var i = PlayerX + 1; i < _xSize; i++)
        {
            _cards[i, PlayerY].GetComponent<CardAnimation>().MoveTo(-1, 0);
            
            _cards[i - 1, PlayerY] = _cards[i, PlayerY];
            _cards[i - 1, PlayerY].GetComponent<Card>().SetCoordinates(x: i - 1);
        }

        _player.GetComponent<CardAnimation>().MoveTo(-1, 0);
        
        _cardFactory.CreateCard(_xSize - 1, PlayerY);
        PlayerX--;
        _cards[PlayerX, PlayerY] = _player.gameObject;
        IsMove = false;
    }

    private async void MoveUp()
    {
        var eatenCard = _cards[PlayerX, PlayerY + 1].GetComponent<CardAnimation>();
        await eatenCard.Death();

        for (var i = 0; i < PlayerY; i++)
        {
            _cards[PlayerX, i].GetComponent<CardAnimation>().MoveTo(0, 1);
            _cards[PlayerX, i + 1] = _cards[PlayerX, i];
            _cards[PlayerX, i + 1].GetComponent<Card>().SetCoordinates(y: i + 1);
        }

        _player.GetComponent<CardAnimation>().MoveTo(0, 1);

        _cardFactory.CreateCard(PlayerX, 0);
        PlayerY++;
        _cards[PlayerX, PlayerY] = _player.gameObject;
        IsMove = false;
    }

    private async void MoveDown()
    {
        var eatenCard = _cards[PlayerX, PlayerY - 1].GetComponent<CardAnimation>();
        await eatenCard.Death();

        for (var i = PlayerY + 1; i < _ySize; i++)
        {
            _cards[PlayerX, i].GetComponent<CardAnimation>().MoveTo(0, -1);
            
            _cards[PlayerX, i - 1] = _cards[PlayerX, i];
            _cards[PlayerX, i - 1].GetComponent<Card>().SetCoordinates(y: i - 1);
        }

        _player.GetComponent<CardAnimation>().MoveTo(0, -1);

        _cardFactory.CreateCard(PlayerX, _ySize - 1);
        PlayerY--;
        _cards[PlayerX, PlayerY] = _player.gameObject;
        IsMove = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {

    }
}
