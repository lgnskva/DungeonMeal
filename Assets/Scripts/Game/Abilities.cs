using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class Abilities : MonoBehaviour
{
    private Save _save;
    private Player _player;

    private int _recharge;
    private int _stepCount;
    private UnityEvent<int, Player> _ability;

    public int LvlAbility;

    [SerializeField] private Text AbilityText;
    [SerializeField] private Button AbilityButton;

    [SerializeField] private GameObject Egg;

    public bool IsWait;

    [Inject]
    private void Construct(Save save, Player player)
    {
        _save = save;
        _player = player;

        _ability = _save.CurrentCharacter.Ability;
        LvlAbility = _save.CharacterData.First(character => character.Id == _save.CurrentCharacter.Id).LvlAbility;
        _recharge = _save.CurrentCharacter.Recharge;
    }
    private void Start()
    {
        _stepCount = _recharge;
        UpdateText();
    }
    private void OnEnable()
    {
        Description.OnCardClicked += ChangePosition;
        Player.OnStep += CountStepAbility;
    }
    private void OnDisable()
    {
        Description.OnCardClicked -= ChangePosition;
        Player.OnStep -= CountStepAbility;
    }
    private void UpdateText()
    {
        if (IsWait)
        {
            AbilityText.text = "Выберите карту";
            AbilityButton.interactable = false;
        }
        else if (_stepCount != 0)
        {
            AbilityText.text = _stepCount.ToString();
            AbilityButton.interactable = false;
        }
        else
        {
            AbilityText.text = "!";
            AbilityButton.interactable = true;
        }
    }

    private static void ChangeCards(GameObject card, GameObject newCard, int damage)
    {
        int x = card.GetComponent<Cards>().x;
        int y = card.GetComponent<Cards>().y;

        Destroy(card);
        Spawner.CreateCard(newCard, x, y, damage);
    }
    public void SpawnEggs()
    {
        GameObject[] poisonCards = GameObject.FindGameObjectsWithTag("Poison");

        if (poisonCards.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, poisonCards.Length);
            while (poisonCards[randomIndex].GetComponent<Cards>().damage <= 0)
            {
                randomIndex = UnityEngine.Random.Range(0, poisonCards.Length);
                if (poisonCards.Length < 2)
                    return;
            }
            ChangeCards(poisonCards[randomIndex], Egg, LvlAbility);
        }
    }
    /*public void DamageAll()
    {
        foreach (GameObject card in GameObject.FindGameObjectsWithTag("Poison"))
        {
            Cards component = card.GetComponent<Cards>();
            component.damage -= _lvlAbility;

            if (component.damage <= 0)
            {
                component.damage = 0;
                StartCoroutine(CreateCard(component));
            }
        }
    }*/
    
    private void ActivateChangePosition()
    {
        _player.isMove = true;
        IsWait = true;
    }
    public void ChangePosition(int xCard, int yCard)
    {
        if (IsWait)
        {
            GameObject temp = Spawner.cards[xCard, yCard];
            Vector3 tempPos = temp.transform.position;

            Spawner.cards[xCard, yCard] = Spawner.cards[_player.x, _player.y];
            Spawner.cards[_player.x, _player.y] = temp;

            Spawner.cards[_player.x, _player.y].transform.position = Spawner.cards[xCard, yCard].transform.position;
            Spawner.cards[xCard, yCard].transform.position = tempPos;

            Spawner.cards[_player.x, _player.y].GetComponent<Cards>().x = _player.x;
            Spawner.cards[_player.x, _player.y].GetComponent<Cards>().y = _player.y;

            _player.x = xCard;
            _player.y = yCard;

            IsWait = false;
            _player.isMove = false;

            if (_player.damage < LvlAbility)
                _player.damage += LvlAbility;
        }
    }

    public void DoAbility()
    {
        if (_stepCount == 0)
        {
            _ability.Invoke(LvlAbility, _player);
            _stepCount = _recharge;
            UpdateText();
        }
    }
    private void CountStepAbility()
    {
        if (_stepCount != 0)
            _stepCount--;
        UpdateText();
    }
}
