using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    public static int xSize = 3;
    public static int ySize = 3;

    public static float xCards = 180f;
    public static float yCards = 280f;

    public static int width = Screen.width;
    public static int height = Screen.height;

    public static GameObject[,] cards = new GameObject[xSize, ySize];

    [SerializeField] private Transform _parentCardsSerialize;
    [SerializeField] private GameObject[] _cardPrefabsSerialize;
    private static Transform _parentCards;
    private static GameObject[] _cardPrefabs;

    private static IInstantiator _instantiator;

    [Inject]
    void Construct(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    private void Start()
    {
        _parentCards = _parentCardsSerialize;
        _cardPrefabs = _cardPrefabsSerialize;
        for (int i = 0; i < xSize; i++)
            for (int j = 0; j < ySize; j++)
            {
                if (i == 1 && j == 1)
                    cards[i, j] = gameObject;
                else
                    CreateCard(i, j);
            }
    }
    public static void CreateCard(int x, int y)
    {
        GameObject card = _cardPrefabs[Random.Range(0, _cardPrefabs.Length)];
        cards[x, y] = _instantiator.InstantiatePrefab(
            card,
            new Vector3(x * xCards - xCards, y * yCards - yCards * 1.4f),
            Quaternion.identity,
            _parentCards);
        Cards newCard = cards[x, y].GetComponent<Cards>();
        newCard.x = x;
        newCard.y = y;
    }
    public static void CreateCard(GameObject card, int x, int y)
    {
        cards[x, y] = _instantiator.InstantiatePrefab(
                    card,
                    new Vector3(x * xCards - xCards, y * yCards - yCards * 1.4f),
                    Quaternion.identity,
                    _parentCards);
        Cards newCard = cards[x, y].GetComponent<Cards>();
        newCard.x = x;
        newCard.y = y;
    }
    public static void CreateCard(GameObject card, int x, int y, int damage)
    {
        cards[x, y] = _instantiator.InstantiatePrefab(
                    card,
                    new Vector3(x * xCards - xCards, y * yCards - yCards * 1.4f),
                    Quaternion.identity,
                    GameObject.Find("Cards").transform);
        Cards newCard = cards[x, y].GetComponent<Cards>();
        newCard.x = x;
        newCard.y = y;
        newCard.damage = damage;
    }
}
