using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class Player : MonoBehaviour
{
    public int x = 1;
    public int y = 1;

    public bool isMove = false;
    private bool _isStop = false;

    public int health;
    public int food;
    public int damage;
    public int countStep;

    [SerializeField] private Text _healthText;
    [SerializeField] private Text _foodText;
    [SerializeField] private Text _damageText;

    private GameObject[,] _cards = Spawner.cards;

    public static Action OnStep;

    private Save _save;


    [Inject]
    void Construct(Save save)
    {
        _save = save;
    }

    void Start()
    {
        health = _save.Health;
        gameObject.GetComponentsInChildren<Image>()[1].sprite = _save.CurrentCharacter.Sprite;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _save.Food += food;
            SceneManager.LoadScene("Menu");
        }
    }
    private void OnApplicationQuit()
    {
        _save.Food += food;
    }
    void UpdateText()
    {
        _damageText.text = damage.ToString();
        _healthText.text = health.ToString();
        _foodText.text = food.ToString();
    }
    public void Swipe(int direction)
    {
        if (direction == 1 && x < Spawner.xSize - 1 && !isMove)
        {
            isMove = true;
            Eat(_cards[x + 1, y]);
            if (!_isStop)
                StartCoroutine(Right());
            else isMove = false;
        }
        if (direction == 2 && x > 0 && !isMove)
        {
            isMove = true;
            Eat(_cards[x - 1, y]);
            if (!_isStop)
                StartCoroutine(Left());
            else isMove = false;
        }
        if (direction == 3 && y < Spawner.ySize - 1 && !isMove)
        {
            isMove = true;
            Eat(_cards[x, y + 1]);
            if (!_isStop)
                StartCoroutine(Up());
            else isMove = false;
        }
        if (direction == 4 && y > 0 && !isMove)
        {
            isMove = true;
            Eat(_cards[x, y - 1]);
            if (!_isStop)
                StartCoroutine(Down());
            else isMove = false;
        }
        UpdateText();
        _isStop = false;
    }
    IEnumerator Right()
    {
        Cards eatenCard = _cards[x + 1, y].GetComponent<Cards>();
        eatenCard.damage = 0;
        yield return StartCoroutine(eatenCard.DeathCard());

        for (int i = x - 1; i >= 0; i--)
        {
            StartCoroutine(MoveAnimation(_cards[i, y], Spawner.xCards, 0));
            _cards[i + 1, y] = _cards[i, y];
            _cards[i + 1, y].GetComponent<Cards>().x = i + 1;
        }

        StartCoroutine(MoveAnimation(gameObject, Spawner.xCards, 0));
        yield return StartCoroutine(MoveAnimation(gameObject, Spawner.xCards, 0));

        Spawner.CreateCard(0, y);
        x++;
        _cards[x, y] = gameObject;
    }
    IEnumerator Left()
    {
        Cards eatenCard = _cards[x - 1, y].GetComponent<Cards>();
        eatenCard.damage = 0;
        yield return StartCoroutine(eatenCard.DeathCard());

        for (int i = x + 1; i < Spawner.xSize; i++)
        {
            StartCoroutine(MoveAnimation(_cards[i, y], -Spawner.xCards, 0));
            _cards[i - 1, y] = _cards[i, y];
            _cards[i - 1, y].GetComponent<Cards>().x = i - 1;
        }

        StartCoroutine(MoveAnimation(gameObject, -Spawner.xCards, 0));
        yield return StartCoroutine(MoveAnimation(gameObject, -Spawner.xCards, 0));

        Spawner.CreateCard(Spawner.xSize - 1, y);
        x--;
        _cards[x, y] = gameObject;
    }
    IEnumerator Up()
    {
        Cards eatenCard = _cards[x, y + 1].GetComponent<Cards>();
        eatenCard.damage = 0;
        yield return StartCoroutine(eatenCard.DeathCard());

        for (int i = 0; i < y; i++)
        {
            StartCoroutine(MoveAnimation(_cards[x, i], 0, Spawner.yCards));
            _cards[x, i + 1] = _cards[x, i];
            _cards[x, i + 1].GetComponent<Cards>().y = i + 1;
        }

        StartCoroutine(MoveAnimation(gameObject, 0, Spawner.yCards));
        yield return StartCoroutine(MoveAnimation(gameObject, 0, Spawner.yCards));

        Spawner.CreateCard(x, 0);
        y++;
        _cards[x, y] = gameObject;
    }
    IEnumerator Down()
    {
        Cards eatenCard = _cards[x, y - 1].GetComponent<Cards>();
        eatenCard.damage = 0;
        yield return StartCoroutine(eatenCard.DeathCard());

        for (int i = y + 1; i < Spawner.ySize; i++)
        {
            StartCoroutine(MoveAnimation(_cards[x, i], 0, -Spawner.yCards));
            _cards[x, i - 1] = _cards[x, i];
            _cards[x, i - 1].GetComponent<Cards>().y = i - 1;
        }

        StartCoroutine(MoveAnimation(gameObject, 0, -Spawner.yCards));
        yield return StartCoroutine(MoveAnimation(gameObject, 0, -Spawner.yCards));

        Spawner.CreateCard(x, Spawner.ySize - 1);
        y--;
        _cards[x, y] = gameObject;
    }


    IEnumerator MoveAnimation(GameObject obj, float xObj, float yObj)
    {
        if (!obj.IsDestroyed())
        {
            float timeMove = 0.2f;

            Vector3 startPos = obj.transform.position;
            Vector3 endPos = new Vector3(obj.transform.position.x + xObj, obj.transform.position.y + yObj, 0);
            for (float i = 0; i < timeMove; i += Time.deltaTime)
            {
                obj.transform.position = Vector3.Lerp(startPos, endPos, i / timeMove);
                yield return null;
            }
            obj.transform.position = endPos;
            isMove = false;
        }
    }

    void Eat(GameObject eaten)
    {
        int eatenDamage = eaten.GetComponent<Cards>().damage;

        if (eaten.tag == "Food")
        {
            food += eatenDamage;
        }

        if (eaten.tag == "Poison")
        {
            if (damage > 0)
            {
                if (damage >= eatenDamage)
                    damage -= eatenDamage;
                else
                {
                    eaten.GetComponent<Cards>().damage -= damage;
                    damage = 0;
                    _isStop = true;
                }
            }
            else
            {
                health -= eatenDamage;
                food += eatenDamage;
            }
        }

        if (eaten.tag == "Trash" && damage < eatenDamage)
        {
            damage = eatenDamage;
        }

        if (eaten.tag == "Pill")
        {
            health += eatenDamage;
            if (health > _save.Health)
                health = _save.Health;
        }

        if (health <= 0)
        {
            health = 0;
            _save.Food += food;
            SceneManager.LoadSceneAsync("GameOver");
        }

        countStep++;
        OnStep?.Invoke();
    }
}
