using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public Player player;
    public CardFactory cardFactory;
    
    [Header("Controllers")]
    public MoveController moveController;
    public AbilityController abilityController;
    public ItemController itemController;

    [Header("Configs")] 
    public SpawnConfig spawnConfig;
    
    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(player).AsSingle();
        Container.Bind<CardFactory>().FromInstance(cardFactory).AsSingle();
        
        BindControllers();
        BindConfigs();
    }

    private void BindControllers()
    {
        Container.Bind<MoveController>().FromInstance(moveController).AsSingle();
        Container.Bind<AbilityController>().FromInstance(abilityController).AsSingle();
        Container.Bind<ItemController>().FromInstance(itemController).AsSingle();
    }

    private void BindConfigs()
    {
        Container.Bind<SpawnConfig>().FromInstance(spawnConfig).AsSingle();
    }
}

