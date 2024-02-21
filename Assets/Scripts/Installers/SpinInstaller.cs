using Zenject;

public class SpinInstaller : MonoInstaller
{
    public CharacterChoose characterChoose;
    public CharactersGrid charactersGrid;

    public override void InstallBindings()
    {
        Container.Bind<CharacterChoose>().FromInstance(characterChoose).AsSingle().NonLazy();
        Container.Bind<CharactersGrid>().FromInstance(charactersGrid).AsSingle().NonLazy();
    }
}