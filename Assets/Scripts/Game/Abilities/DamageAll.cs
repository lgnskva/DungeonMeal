using System.Threading.Tasks;
using Zenject;
using UnityEngine;

public class DamageAll : Ability
{
    private int _lvlAbility;
    private CardFactory _cardFactory;
    
    [Inject]
    private void Construct(CardFactory cardFactory)
    {
        _cardFactory = cardFactory;
        _lvlAbility = DataController.CurrentCharacter.LvlAbility;
    }
    public override async void DoAbility()
    {
        foreach (var card in GameObject.FindGameObjectsWithTag("Spoiled"))
        {
            var damagedCard = card.GetComponent<Card>();
            damagedCard.DecreaseDamage(_lvlAbility);

            if (damagedCard.Damage <= 0)
            {
                await CreateCard(damagedCard);
            }
        }
        OnEndAbility?.Invoke();
    }
    private async Task CreateCard(Card damagedCard)
    {
        await damagedCard.GetComponent<CardAnimation>().Death().ConfigureAwait(true);
        _cardFactory.CreateCard(damagedCard.X, damagedCard.Y);
    }
}