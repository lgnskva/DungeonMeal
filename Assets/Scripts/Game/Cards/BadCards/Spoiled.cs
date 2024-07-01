public class Spoiled : Card
{
    public override void PerformAction()
    {
        if (Player.Damage > 0)
        {
            var playerDamage = Player.Damage;
            Player.DecreaseDamage(Damage);
            DecreaseDamage(playerDamage);
            
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
