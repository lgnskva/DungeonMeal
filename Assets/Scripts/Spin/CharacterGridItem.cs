using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class CharacterGridItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _lockMask;
    [SerializeField] private Image _sprite;
    [SerializeField] private GameObject _grid;

    private string _id;
    private int _lvlAbility;

    private Save _save;
    private CharacterChoose _characterChoose;

    [Inject]
    void Construct(Save save, CharacterChoose characterChoose)
    {
        _save = save;
        _characterChoose = characterChoose;
    }
    public void Create(string id, Sprite sprite)
    {
        _id = id;
        _sprite.sprite = sprite;

        _lvlAbility = _save.CharacterData.Where(character => character.Id == _id).First().LvlAbility;
        LockOrUnlock();
    }
    void LockOrUnlock()
    {
        if (_lvlAbility == 0)
            _lockMask.SetActive(true);
        else
            _lockMask.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _characterChoose.OpenCharacter(_id);
    }
}
