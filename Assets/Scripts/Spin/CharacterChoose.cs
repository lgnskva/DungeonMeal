using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class CharacterChoose : MonoBehaviour
{
    private Save _save;

    private List<GameObject> _charactersItems = new();
    /*[SerializeField] private GameObject _choosePanel;
    [SerializeField] private GameObject _gridPanel;*/
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private Transform _characterParent;
    
    public static event Action OnOpenCharacter; 

    private int _currentIndex;

    private IInstantiator _instantiator;

    [Inject]
    private void Construct(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }
    private void Start()
    {
        CreateCharacters();
    }

    private void OnEnable()
    {
        SpinUI.ChoosePanelHidden += HideCurrentCharacter;
    }

    private void OnDisable()
    {
        SpinUI.ChoosePanelHidden -= HideCurrentCharacter;
    }

    private void CreateCharacters()
    {
        var characters = Resources.Load<PlayerConfig>("Configs/PlayerConfig").Characters;
        foreach (var character in characters)
        {
            var characterObject = _instantiator.InstantiatePrefab(_characterPrefab, _characterParent);
            _charactersItems.Add(characterObject);
            characterObject.gameObject.SetActive(false);

            var characterItem = characterObject.GetComponent<CharacterChooseItem>();
            characterItem.Create(character);
        }
    }
    
    public void OpenCharacter(string id)
    {
        var characterItem = _charactersItems.First(character => character.GetComponent<CharacterChooseItem>().Id == id);
        _currentIndex = _charactersItems.IndexOf(characterItem);
        
        characterItem.SetActive(true);
        OnOpenCharacter.Invoke();
        /*_gridPanel.SetActive(false);
        _choosePanel.SetActive(true);*/
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
    private void HideCurrentCharacter()
    {
        _charactersItems[_currentIndex].SetActive(false);
    }
}
