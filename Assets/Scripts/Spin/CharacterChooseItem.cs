using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CharacterChooseItem : MonoBehaviour
{
    [SerializeField] private Image _characterSprite;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _lvlText;
    [SerializeField] private GameObject _button;

    public string Id { get; private set; }
    public int LvlAbility;

    Save _save;


    [Inject]
    void Construct(Save save)
    {
        _save = save;
    }
    public void Create(string id, string name, string description, Sprite sprite)
    {
        Id = id;
        _characterSprite.sprite = sprite;
        _descriptionText.text = description;
        _nameText.text = name;

        UpdateLvl();
        LockOrUnlock();
    }
    void LockOrUnlock()
    {
        if (LvlAbility == 0)
        {
            _button.GetComponentInChildren<Text>().text = "меднярсомн";
            _button.GetComponent<Button>().interactable = false;
        }
        else if (Id == _save.CurrentCharacter.Id)
        {
            _button.GetComponentInChildren<Text>().text = "бшапюмн";
            _button.GetComponent<Button>().interactable = false;
        }
        else
        {
            _button.GetComponentInChildren<Text>().text = "бшапюрэ";
            _button.GetComponent<Button>().interactable = true;
        }
    }
    public void UpdateLvl()
    {
        LvlAbility = _save.CharacterData.Where(character => character.Id == Id).First().LvlAbility;
        _lvlText.text = "сП. " + LvlAbility;
    }
    public void ChooseButton()
    {
        _save.CurrentCharacter = _save.Characters.Where(character => character.Id == Id).First();
    }
}
