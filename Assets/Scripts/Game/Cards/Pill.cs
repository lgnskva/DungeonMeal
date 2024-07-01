public class Pill : Card
{
    public override void PerformAction()
    {
        Player.IncreaseHealth(Damage);
    }
}
