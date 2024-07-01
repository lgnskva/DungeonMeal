using UnityEngine;
using Zenject;

public class SpawnEggs : Ability
{
    [SerializeField] private GameObject _egg;
    private int _lvlAbility;
    private CardFactory _cardFactory;

    [Inject]
    private void Construct(CardFactory cardFactory)
    {
        _cardFactory = cardFactory;
        _lvlAbility = DataController.CurrentCharacter.LvlAbility;
    }
    public override void DoAbility()
    {
        _lvlAbility = DataController.CurrentCharacter.LvlAbility;
        var poisonCards = GameObject.FindGameObjectsWithTag("Spoiled");

        if (poisonCards.Length > 0)
        {
            var randomIndex = Random.Range(0, poisonCards.Length);
            ChangeCards(poisonCards[randomIndex], _egg, _lvlAbility);
        }
    }

    private void ChangeCards(GameObject card, GameObject newCard, int damage)
    {
        var x = card.GetComponent<Card>().X;
        var y = card.GetComponent<Card>().Y;

        Destroy(card);
        _cardFactory.CreateCard(newCard, x, y, damage);
        OnEndAbility?.Invoke();
    }
}