public class TrashCan : Card
{
    public override void PerformAction()
    {
        Player.IncreaseDamage(Damage);
    }
}
