using UnityEngine;
using Zenject;

public class ItemCard : Card
{
    [SerializeField] private GameObject _itemPrefab;
    private ItemController _itemController;
    private Player _player;

    [Inject]
    private void Construct(ItemController itemController, Player player)
    {
        _itemController = itemController;
        _player = player;
    }
    
    public override void PerformAction()
    {
        _player.AddFood(Damage);
        _itemController.AddSlot(_itemPrefab);
    }
}
