using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class CharacterChoose : MonoBehaviour
{
    private Save _save;

    private List<GameObject> _charactersItems = new();
    [SerializeField] private GameObject _choosePanel;
    [SerializeField] private GameObject _gridPanel;
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private Transform _characterParent;

    private int _currentIndex = 0;

    IInstantiator _instantiator;

    [Inject]
    void Construct(Save save, IInstantiator instantiator)
    {
        _save = save;

        _instantiator = instantiator;
    }
    void Start()
    {
        CreateCharacters();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButton();
        }
    }
    private void CreateCharacters()
    {
        CharacterInfo[] characters = _save.Characters;
        foreach (CharacterInfo character in characters)
        {
            GameObject _character = _instantiator.InstantiatePrefab(_characterPrefab, _characterParent);
            _charactersItems.Add(_character);
            _character.gameObject.SetActive(false);

            CharacterChooseItem _characterItem = _character.GetComponent<CharacterChooseItem>();
            _characterItem.Create(character.Id, character.Name, character.Description, character.Sprite);
        }
    }
    public void OpenCharacter(string id)
    {
        _charactersItems.Where(character => character.GetComponent<CharacterChooseItem>().Id == id).First().SetActive(true);

        _gridPanel.SetActive(false);
        _choosePanel.SetActive(true);
    }
    public void UpdateCharacterLvl(string id)
    {
        _charactersItems.Select(character => character.GetComponent<CharacterChooseItem>()).Where(characterItem => characterItem.Id == id).First().UpdateLvl();
    }
    public void LeftButton()
    {
        _charactersItems[_currentIndex].SetActive(false);
        _currentIndex--;
        if (_currentIndex < 0)
            _currentIndex = _charactersItems.Count - 1;
        _charactersItems[_currentIndex].SetActive(true);
    }
    public void RightButton()
    {
        _charactersItems[_currentIndex].SetActive(false);
        _currentIndex++;
        if (_currentIndex == _charactersItems.Count)
            _currentIndex = 0;
        _charactersItems[_currentIndex].SetActive(true);
    }
    public void BackButton()
    {
        if (_choosePanel.activeSelf == true)
        {
            _charactersItems[_currentIndex].SetActive(false);
            _choosePanel.SetActive(false);
            _gridPanel.SetActive(true);
        }
        else
            SceneManager.LoadScene("Menu");
    }
}
