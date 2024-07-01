using UnityEngine;

public class CloseButtonItemDescription : MonoBehaviour
{
    [SerializeField] private GameObject _descriptionPanel;
    
    public void Close()
    {
        _descriptionPanel.SetActive(false);
    }
}
