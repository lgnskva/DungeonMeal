using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AbilityController : MonoBehaviour
{
    private IInstantiator _instantiator;

    private int _recharge;
    private int _stepCount;
    private Ability _ability;

    [SerializeField] private Text _stepCountText;
    [SerializeField] private Button _abilityButton;

    [Inject]
    private void Construct(IInstantiator instantiator)
    {
        _instantiator = instantiator;
        
        _recharge = DataController.CurrentCharacter.Recharge;
    }
    
    private void Start()
    {
        _ability = DataController.CurrentCharacter.Ability;
        _ability = _instantiator.InstantiatePrefab(_ability.gameObject, transform).GetComponent<Ability>();
        
        _stepCount = _recharge;
        UpdateText();
    }
    
    private void OnEnable()
    {
        MoveController.OnStep += CountStepAbility;
        Ability.OnEndAbility += EndAbility;
    }

    private void OnDisable()
    {
        MoveController.OnStep -= CountStepAbility;
        Ability.OnEndAbility -= EndAbility;
    }
    
    public void UpdateText(string text)
    {
        if (MoveController.IsAbilityWait)
        {
            _stepCountText.text = text;
            _abilityButton.interactable = false;
        }
    }
    
    private void UpdateText()
    {
        if (MoveController.IsAbilityWait)
            return;
        if (_stepCount != 0)
        {
            _stepCountText.text = _stepCount.ToString();
            _abilityButton.interactable = false;
        }
        else
        {
            _stepCountText.text = "!";
            _abilityButton.interactable = true;
        }
    }
    
    public void DoAbility()
    {
        if (_stepCount == 0)
        {
            MoveController.IsMove = true;
            _ability.DoAbility();
        }
    }
    
    private void CountStepAbility()
    {
        if (_stepCount != 0)
            _stepCount--;
        UpdateText();
    }

    private void EndAbility()
    {
        _stepCount = _recharge;
        UpdateText();
        MoveController.IsMove = false;
    }
}
