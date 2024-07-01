public class BadCard : Card
{
    public override void PerformAction()
    {
        if (Player.Damage > 0)
        {
            Player.DecreaseDamage(Damage);
            DecreaseDamage(Player.Damage);
            
            if (Player.Damage < Damage)
                MoveController.IsStop = true;
        }
        else
        {
            Player.DecreaseHealth(Damage);
            Player.AddFood(Damage);
        }
    }
}
