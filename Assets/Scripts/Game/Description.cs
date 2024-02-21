using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Description : MonoBehaviour
{
    [SerializeField] private GameObject description;
    private Text textDescription;

    public static Action<int, int> OnCardClicked;

    private Player _player;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }
    void Start()
    {
        textDescription = description.GetComponentInChildren<Text>();
    }
    void Update()
    {
        if (_player.isMove)
            description.SetActive(false);
    }
    public void ClickCard()
    {
        /*if (Abilities.isWait)
        {
            Cards clickedCard = GetComponent<Cards>();
            Abilities.ChangePosition(clickedCard.x, clickedCard.y);
            return;

        }*/
        Cards clickedCard = GetComponent<Cards>();
        OnCardClicked?.Invoke(clickedCard.x, clickedCard.y);
        ShowDescription();
    }
    void ShowDescription()
    {
        if (description.activeSelf)
            description.SetActive(false);
        else
        {
            switch (tag)
            {
                case "Food":
                    textDescription.text = $"Пополняет запасы еды на {GetComponent<Cards>().damage}";
                    break;
                case "Poison":
                    textDescription.text = $"Ухудшает здоровье на {GetComponent<Cards>().damage}";
                    break;
                case "Pill":
                    textDescription.text = $"Лечит на {GetComponent<Cards>().damage}";
                    break;
                case "Trash":
                    textDescription.text = $"Дает мусорку, вмещающую {GetComponent<Cards>().damage} испорченной еды";
                    break;
                case "Player":
                    textDescription.text = "Потом сделаю =>";
                    break;
            }
            description.SetActive(true);
        }
    }
}
