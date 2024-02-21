using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Spin : MonoBehaviour
{
    private Save _save;
    [SerializeField] private GameObject _chooseCharacter;
    [SerializeField] private GameObject _prizePanel;
    [SerializeField] private Image _prizeSprite;
    [SerializeField] private Text _foodText;
    [SerializeField] private Text _lvlAbilityText;


    private int price = 100;

    [Inject]
    void Construct(Save save)
    {
        _save = save;
    }
    void Start()
    {
        _foodText.text = _save.Food.ToString();
    }
    
    public void BuySpin()
    {
        if (_save.Food >= price)
        {
            DoSpin();
            _save.Food -= price;
            _foodText.text = _save.Food.ToString();
        }
    }
    void DoSpin()
    {
        CharacterData prize = _save.CharacterData.ElementAt(Random.Range(0, _save.CharacterData.Count));
        prize.LvlAbility++;
        
        _prizePanel.SetActive(true);

        _lvlAbilityText.text = $"Óð. {prize.LvlAbility - 1} >> {prize.LvlAbility}";
        _prizeSprite.sprite = _save.Characters.Where(character => character.Id == prize.Id).FirstOrDefault().Sprite;

        _chooseCharacter.GetComponent<CharacterChoose>().UpdateCharacterLvl(prize.Id);
    }
    public void GetPrize()
    {
        _prizePanel.SetActive(false);

    }
}
