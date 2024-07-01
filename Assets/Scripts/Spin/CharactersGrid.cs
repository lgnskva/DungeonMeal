using UnityEngine;
using Zenject;

public class CharactersGrid : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private Transform _characterParent;
    private CharacterInfo[] _characters;
    private static IInstantiator _instantiator;

    [Inject]
    void Construct(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    private void Start()
    {
        _characters = Resources.Load<PlayerConfig>("Configs/PlayerConfig").Characters;
        
        foreach (var character in _characters)
        {
            var _character = _instantiator.InstantiatePrefab(_characterPrefab, _characterParent);
            var _characterItem = _character.GetComponent<CharacterGridItem>();

            _characterItem.Create(character.Id, character.Sprite);
        }
    }
}
