using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class Player : MonoBehaviour
{
    public static int Health { get; private set; }
    public static int Food { get; private set; }
    public static int Damage { get; private set; }
    
    [SerializeField] private Text _healthText;
    [SerializeField] private Text _foodText;
    [SerializeField] private Text _damageText;
    
    private GameData _gameData;
    private MoveController _moveController;

    [Inject]
    private void Construct(GameData gameData, MoveController moveController)
    {
        _gameData = gameData;
        _moveController = moveController;
        
        DefaultValues();
    }

    private void DefaultValues()
    {
        Health = _gameData.MaxHealth;
        Food = 0;
        Damage = 0;
        
        _healthText.text = Health.ToString();
        
        gameObject.GetComponentsInChildren<Image>()[1].sprite = DataController.CurrentCharacter.Sprite;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }
    }

    public void BackToMenu()
    {
        _gameData.Food += Food;
        SceneManager.LoadScene("Menu");
    }
    private void OnApplicationQuit()
    {
        _gameData.Food += Food;
    }
    public void IncreaseHealth(int health)
    {
        if (health <= 0)
            return;
        Health += health;
        if (Health > _gameData.MaxHealth)
            Health = _gameData.MaxHealth;
        
        _healthText.text = Health.ToString();
    }
    public void DecreaseHealth(int health)
    {
        if (health <= 0)
            return;
        
        Health -= health;
        _healthText.text = Health.ToString();

        if (Health <= 0) 
            GameOver();
    }

    private async void GameOver()
    {
        Health = 0;
        //_gameData.Food += Food;
            
        MoveController.IsStop = true;
        await GetComponent<CardAnimation>().Death();
            
        SceneManager.LoadSceneAsync("GameOver");
    }

    public void IncreaseDamage(int damage)
    {
        if (damage > 0 && damage > Damage)
            Damage = damage;
        
        _damageText.text = Damage.ToString();
    }

    public void DecreaseDamage(int damage)
    {
        if (damage <= 0)
            return;
        
        Damage -= damage;
        
        if (Damage < 0)
            Damage = 0;
        
        _damageText.text = Damage.ToString();
    }
    public void AddFood(int food)
    {
        if (food <= 0)
            return;
        Food += food;
        
        _foodText.text = Food.ToString();
    }
}
