public class MoveDirection : Boss
{
    protected override void PerformBossAction()
    {
        var playerX = MoveController.PlayerX;
        var playerY = MoveController.PlayerY;
        if (X == playerX && (Y == playerY + 1 || Y == playerY - 1) ||
            Y == playerY && (X == playerX + 1 || X == playerX - 1))
            DecreaseDamage(1);
    }
}
