using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Shop : MonoBehaviour
{
    [SerializeField] private Text _foodText;
    [SerializeField] private Text _priceHealthText;

    private Save _save;

    private int _priceHealth;

    [Inject]
    void Construct(Save save)
    {
        _save = save;
    }
    void Start()
    {
        _priceHealth = _save.Health * 100;

        _foodText.text = _save.Food.ToString();
        _priceHealthText.text = _priceHealth.ToString();
    }
    public void BuyHealth()
    {
        Buy(ref _priceHealth, ref _save.Health);
        _priceHealth = _save.Health * 100;
        _priceHealthText.text = _priceHealth.ToString();
    }
    void Buy(ref int price, ref int stat)
    {
        if (_save.Food > price)
        {
            stat++;
            _save.Food -= price;
            _foodText.text = _save.Food.ToString();
        }
    }
}
