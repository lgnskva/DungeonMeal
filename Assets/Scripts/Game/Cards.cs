using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Cards : MonoBehaviour
{
    public int damage;
    [SerializeField] private Text damageText;

    public int x;
    public int y;

    private Save _save;
    private Player _player;

    [Inject]
    public void Construct(Save save, Player player)
    {
        _save = save;
        _player = player;
    }
    private void Start()
    {
        int difficulty = (_player.countStep / 10) + _save.Health - 10;
        damage = Random.Range(1, 6) + difficulty;
    }

    void Update()
    {
        damageText.text = damage.ToString();

        if (damage <= 0 && !_player.isMove)
        {
            damageText.text = "0";
            StartCoroutine(DeathCard());
        }
    }
    public IEnumerator DeathCard()
    {
        _player.isMove = true;
        StartCoroutine(DeathAnimation());
        yield return StartCoroutine(DeathAnimation());
        Destroy(gameObject);
    }
    IEnumerator DeathAnimation()
    {
        Image sprite = gameObject.GetComponentInChildren<Image>();

        while (sprite.color.a >= 0.1f)
        {
            sprite.color = Color.Lerp(sprite.color, Color.clear, 3 * Time.deltaTime);
            yield return null;
        }
        sprite.color = Color.clear;
    }
}
