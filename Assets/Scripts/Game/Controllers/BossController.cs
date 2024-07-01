using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BossController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bosses;
    [SerializeField] private int _stepAppearance;

    private GameObject _currentBoss;

    private CardFactory _cardFactory;

    [Inject]
    private void Construct(CardFactory cardFactory)
    {
        _cardFactory = cardFactory;
    }

    private void OnEnable()
    {
        MoveController.OnStep += BossAppear;
    }

    private void OnDisable()
    {
        MoveController.OnStep -= BossAppear;
    }

    private async void BossAppear()
    {
        if (MoveController.CountStep % _stepAppearance != 0 || MoveController.CountStep == 0)
            return;

        MoveController.IsStop = true;
        var isBoss = true;
        GetRandomCoordinates(out var randomX, out var randomY);
        
        foreach (var cardObj in CardFactory.Cards)
        {
            var card = cardObj.GetComponent<Card>();
            if (!card)
                continue;

            await cardObj.GetComponent<CardAnimation>().Death();

            if (isBoss && card.X == randomX && card.Y == randomY)
            {
                _currentBoss = _bosses[Random.Range(0, _bosses.Count)];
                _cardFactory.CreateCard(_currentBoss, card.X, card.Y);
                isBoss = false;
            }
            else
                _cardFactory.CreateCard(card.X, card.Y);
        }
    }
    
    private static void GetRandomCoordinates(out int randomX, out int randomY)
    {
        var _config = Resources.Load<SpawnConfig>("Configs/SpawnConfig");
        do
        {
            randomX = Random.Range(0, _config.XSize);
            randomY = Random.Range(0, _config.YSize);
        }
        while (CardFactory.Cards[randomX, randomY].name == "Player");
    }
}
