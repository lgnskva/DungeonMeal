using Zenject;

public class Heal : Item
{
    private Player _player;
    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }
    protected override void PerformAction()
    {
        _player.IncreaseHealth(1);
    }
}