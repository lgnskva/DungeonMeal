using Zenject;

public class MainInstaller : MonoInstaller
{
    public Save savePrefab;
    public override void InstallBindings()
    {
        Container.Bind<Save>().FromComponentsInNewPrefab(savePrefab).AsSingle().NonLazy();
    }
}
