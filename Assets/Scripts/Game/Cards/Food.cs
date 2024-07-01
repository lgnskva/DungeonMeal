public class Food : Card
{
    public override void PerformAction()
    {
        Player.AddFood(Damage);
    }
}
