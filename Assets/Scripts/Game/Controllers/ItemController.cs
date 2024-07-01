using System.Linq;
using ModestTree;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ItemController : MonoBehaviour
{
    [SerializeField] private GameObject[] _slots;
    private readonly GameObject[] _items = new GameObject[3];
    
    [Header("Description")]
    [SerializeField] private GameObject _descriptionPanel;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private Button _sellButton;
    
    private IInstantiator _instantiator;
    private Player _player;

    [Inject]
    private void Construct(IInstantiator instantiator, Player player)
    {
        _instantiator = instantiator;
        _player = player;
    }

    public void AddSlot(GameObject newItem)
    {
        if (_items.All(item => item != null))
            return;

        var index = _items.IndexOf(null);
        _items[index] = _instantiator.InstantiatePrefab(newItem, _slots[index].transform);
        SetDescription(_items[index]);
    }

    private void SetDescription(GameObject item)
    {
        var description = item.GetComponent<DescriptionItem>();
        description.DescriptionPanel = _descriptionPanel;
        description.DescriptionText = _descriptionText;
        description.SellButton = _sellButton;
    }

    public void SellItem(GameObject item)
    {
        _items[_items.IndexOf(item)] = null;
        Destroy(item);
        _player.AddFood(100);
    }
}