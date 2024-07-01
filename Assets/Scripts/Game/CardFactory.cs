using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CardFactory : MonoBehaviour
{
    [SerializeField] private Transform _parentCards;
    [SerializeField] private GameObject _player;
    
    private Dictionary<GameObject[], int> _cardsChances;
    public static GameObject[,] Cards;
    
    private static IInstantiator _instantiator;
    private SpawnConfig _config;

    [Inject]
    private void Construct(IInstantiator instantiator, SpawnConfig config)
    {
        _instantiator = instantiator;
        _config = config;
        
        _cardsChances = _config.GetCardsChances();
        Cards = new GameObject[_config.XSize, _config.YSize];
    }
    private void Start()
    {
        SpawnAllCards();
    }
    private void SpawnAllCards()
    {
        var playerX = MoveController.PlayerX;
        var playerY = MoveController.PlayerY;
        
        for (var i = 0; i < _config.XSize; i++)
            for (var j = 0; j < _config.YSize; j++)
            {
                if (i == playerX && j == playerY)
                    Cards[i, j] = _player;
                else
                    CreateCard(i, j);
            }    
    }
    public void CreateCard(int x, int y)
    {
        var card = GetRandomCard();
        InstantiateCard(card, x, y);
        
        var newCard = Cards[x, y].GetComponent<Card>();
        newCard.SetCoordinates(x, y);
    }
    public void CreateCard(GameObject card, int x, int y)
    {
        InstantiateCard(card, x, y);
        
        var newCard = Cards[x, y].GetComponent<Card>();
        newCard.SetCoordinates(x, y);
    }
    public void CreateCard(GameObject card, int x, int y, int damage)
    {
        InstantiateCard(card, x, y);
        
        var newCard = Cards[x, y].GetComponent<Card>();
        newCard.SetCoordinates(x, y);
        newCard.ChangeDamage(damage);
    }

    private void InstantiateCard(Object card, int x, int y)
    {
        var parentPosition = _parentCards.position;
        
        var newCard = _instantiator.InstantiatePrefab(
            card,
            new Vector3(parentPosition.x + (x - 1) * _config.WidthCard, parentPosition.y + (y - 1) * _config.HeightCard),
            Quaternion.identity,
            _parentCards);

        Cards[x, y] = newCard;
        newCard.transform.localScale = new Vector3(1, 1);
        newCard.GetComponent<CardAnimation>().Appear();
    }

    private GameObject GetRandomCard()
    {
        var allChance = _cardsChances.Sum(chance => chance.Value);
        var randomTypeChance = Random.Range(0, allChance + 1);

        var sumChances = 0;

        foreach (var cardsChance in _cardsChances)
        {
            if (randomTypeChance <= cardsChance.Value + sumChances)
            {
                return cardsChance.Key[Random.Range(0, cardsChance.Key.Length)];
            }
            
            sumChances += cardsChance.Value;
        }
        return null;
    }
}
