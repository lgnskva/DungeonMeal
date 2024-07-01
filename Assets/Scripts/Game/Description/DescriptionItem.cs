using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DescriptionItem : Description
{
    [SerializeField] public string OriginalText;
    [HideInInspector] public Button SellButton;
    
    private ItemController _itemController;

    [Inject]
    private void Construct(ItemController itemController)
    {
        _itemController = itemController;
    }
    private void Start()
    {
        Text = OriginalText;
    }

    public override void OnClick()
    {
        SellButton.onClick.RemoveAllListeners();
        SellButton.onClick.AddListener(Sell);
        
        ShowDescription();
    }

    private void Sell()
    {
        _itemController.SellItem(gameObject);
        ShowDescription();
    }
}
