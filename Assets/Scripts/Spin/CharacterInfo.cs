using System.Linq;
using UnityEngine;
using Zenject;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField] private string _id;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
    [SerializeField, TextArea] private string _description;
    [SerializeField] private Ability _ability;
    [SerializeField] private int _recharge;
    
    private int _lvlAbility;

    public string Id => _id;
    public Sprite Sprite => _sprite;
    public string Name => _name;
    public string Description => _description;
    public Ability Ability => _ability;
    public int Recharge => _recharge;
    public int LvlAbility => _lvlAbility;

    public void SetLvlAbility(int lvlAbility)
    {
        _lvlAbility = lvlAbility;
    }
    public string GetDescription()
    {
        return _description.Replace("{}", _lvlAbility.ToString());
    }

}
