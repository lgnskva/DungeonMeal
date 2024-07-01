using UnityEngine;
using Zenject;

public class Poison : Ability
{
    private int _lvlAbility;
    private SpawnConfig _config;

    [Inject]
    private void Construct(SpawnConfig spawnConfig)
    {
        _config = spawnConfig;
    }
    
    public override void DoAbility()
    {
        _lvlAbility = DataController.CurrentCharacter.LvlAbility;
        var playerX = MoveController.PlayerX;
        var playerY = MoveController.PlayerY;

        if (playerX < _config.XSize - 1)
            PoisonCard(playerX + 1, playerY);
        if (playerX > 0)
            PoisonCard(playerX - 1, playerY);
        if (playerY < _config.YSize-1)
            PoisonCard(playerX, playerY + 1);
        if (playerY > 0)
            PoisonCard(playerX, playerY - 1);
        
        
    }
    private void PoisonCard(int x, int y)
    {
        if (!CardFactory.Cards[x, y].CompareTag("Spoiled"))
            return;
        
        var card = CardFactory.Cards[x, y].GetComponent<Card>();
        
        card.IsPoisoned = true;
        
        if (card.Damage <= _lvlAbility)
            card.ChangeDamage(1);
        else
            card.DecreaseDamage(_lvlAbility);
        
        OnEndAbility.Invoke();
    }
}
