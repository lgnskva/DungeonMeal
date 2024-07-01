using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    public GameObject DescriptionPanel;
    public Text DescriptionText;
    protected string Text;

    private void OnEnable()
    {
        MoveController.OnStep += HideDescription;
    }

    private void OnDisable()
    {
        MoveController.OnStep -= HideDescription;
    }

    private void HideDescription()
    {
        DescriptionPanel.SetActive(false);
    }
    public virtual void OnClick()
    {
        ShowDescription();
    }

    protected void ShowDescription()
    {
        if (DescriptionPanel.activeSelf)
            DescriptionPanel.SetActive(false);
        else
        {
            DescriptionText.text = Text;
            DescriptionPanel.SetActive(true);
        }
    }
}
