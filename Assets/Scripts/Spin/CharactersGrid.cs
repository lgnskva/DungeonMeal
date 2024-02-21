using UnityEngine;
using Zenject;

public class CharactersGrid : MonoBehaviour
{
    [SerializeField] private Transform _characterParent;
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _choosePanel;
    [SerializeField] private GameObject _gridPanel;
    private CharacterInfo[] _characters;
    private Save _save;
    private CharacterChoose _characterChoose;
    private static IInstantiator _instantiator;


    [Inject]
    void Construct(Save save, IInstantiator instantiator, CharacterChoose characterChoose)
    {
        _save = save;
        _characters = _save.Characters;

        _characterChoose = characterChoose;

        _instantiator = instantiator;
    }

    private void Start()
    {
        foreach (CharacterInfo character in _characters)
        {
            GameObject _character = _instantiator.InstantiatePrefab(_characterPrefab, _characterParent);
            CharacterGridItem _characterItem = _character.GetComponent<CharacterGridItem>();

            _characterItem.Create(character.Id, character.Sprite);
        }
    }
}
