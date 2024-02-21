using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New CharacterData", menuName = "Character Data")]
public class CharacterInfo : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private UnityEvent<int, Player> _ability;
    [SerializeField] private int _recharge;

    public string Id => _id;
    public Sprite Sprite => _sprite;
    public string Name => _name;
    public string Description => _description;
    public UnityEvent<int, Player> Ability => _ability;
    public int Recharge => _recharge;

}
