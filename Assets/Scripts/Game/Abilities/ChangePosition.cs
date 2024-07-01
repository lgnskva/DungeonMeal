using Zenject;

public class ChangePosition : Ability
{
    private AbilityController _abilityController;
    private Player _player;
    private int _lvlAbility;
    
    [Inject]
    private void Construct(Player player, AbilityController abilityController)
    {
        _player = player;
        _abilityController = abilityController;
        _lvlAbility = DataController.CurrentCharacter.LvlAbility;
    }
    private void OnEnable()
    {
        DescriptionCard.OnCardClicked += ActivateChangePosition;
    }
    private void OnDisable()
    {
        DescriptionCard.OnCardClicked -= ActivateChangePosition;
    }
    public override void DoAbility()
    {
        MoveController.IsAbilityWait = true;
        _abilityController.UpdateText("Выберите карту");
    }
    private void ActivateChangePosition(int xCard, int yCard)
    {
        if (!MoveController.IsAbilityWait) 
            return;
        
        var temp = CardFactory.Cards[xCard, yCard];
        var tempPos = temp.transform.position;

        CardFactory.Cards[xCard, yCard] = CardFactory.Cards[MoveController.PlayerX, MoveController.PlayerY];
        CardFactory.Cards[MoveController.PlayerX, MoveController.PlayerY] = temp;

        CardFactory.Cards[MoveController.PlayerX, MoveController.PlayerY].transform.position = CardFactory.Cards[xCard, yCard].transform.position;
        CardFactory.Cards[xCard, yCard].transform.position = tempPos;

        CardFactory.Cards[MoveController.PlayerX, MoveController.PlayerY]
            .GetComponent<Card>()
            .SetCoordinates(MoveController.PlayerX, MoveController.PlayerY);

        MoveController.PlayerX = xCard;
        MoveController.PlayerY = yCard;

        MoveController.IsAbilityWait = false;
        
        _player.IncreaseDamage(_lvlAbility);
        
        OnEndAbility?.Invoke();
    }
}