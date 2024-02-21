using Zenject;

public class GameInstaller : MonoInstaller
{
    public Player player;
    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(player).AsSingle();
    }
}

