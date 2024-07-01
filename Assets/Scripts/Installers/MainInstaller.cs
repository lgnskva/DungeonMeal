using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameData>().FromMethod(Save.LoadData).AsSingle();
    }
}
