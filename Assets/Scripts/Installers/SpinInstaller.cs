using Zenject;

public class SpinInstaller : MonoInstaller
{
    public CharacterChoose characterChoose;

    public override void InstallBindings()
    {
        Container.Bind<CharacterChoose>().FromInstance(characterChoose).AsSingle().NonLazy();
    }
}