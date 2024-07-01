using System;
using UnityEngine;

public class DescriptionCard : Description
{
    [SerializeField, TextArea] protected string OriginalText;

    private Card _card;
    public static Action<int, int> OnCardClicked;

    public override void OnClick()
    {
        _card = GetComponent<Card>();
        if (!MoveController.IsAbilityWait)
        {
            Text = OriginalText.Replace("{}", _card.Damage.ToString());
            ShowDescription();
        }
        OnCardClicked?.Invoke(_card.X, _card.Y);
    }
}
