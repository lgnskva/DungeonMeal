using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class SpinUI : MonoBehaviour
{
    [Header("Spin")]
    [SerializeField] private Spin _spin;
    [SerializeField] private Text _foodText;
    [SerializeField] private Text _priceText;

    [SerializeField] private GameObject _gridPanel;
    [SerializeField] private GameObject _choosePanel;
    
    [Header("Prize")]
    [SerializeField] private GameObject _prizePanel;
    [SerializeField] private Image _prizeSprite;
    [SerializeField] private Text _lvlAbilityText;

    private GameData _gameData;

    public static event Action ChoosePanelHidden;
    
    [Inject]
    private void Construct(GameData gameData)
    {
        _gameData = gameData;
    }

    private void Start()
    {
        _foodText.text = _gameData.Food.ToString();
        _priceText.text = _spin.Price.ToString();
    }

    private void OnEnable()
    {
        CharacterChoose.OnOpenCharacter += ShowChoosePanel;
    }

    private void OnDisable()
    {
        CharacterChoose.OnOpenCharacter -= ShowChoosePanel;
    }

    public void ShowPrize(int lvlAbility, Sprite prizeSprite)
    {
        _foodText.text = _gameData.Food.ToString();
        
        _prizePanel.SetActive(true);

        _lvlAbilityText.text = $"Ур. {lvlAbility - 1} >> {lvlAbility}";
        _prizeSprite.sprite = prizeSprite;
    }

    public void HidePrizePanel()
    {
        _prizePanel.SetActive(false);
    }

    public void BackButton()
    {
        if (_choosePanel.activeSelf)
        {
            ChoosePanelHidden.Invoke();
            _choosePanel.SetActive(false);
            _gridPanel.SetActive(true);
        }
        else
            SceneManager.LoadScene("Menu");
    }

    private void ShowChoosePanel()
    {
        _gridPanel.SetActive(false);
        _choosePanel.SetActive(true);
    }
}
