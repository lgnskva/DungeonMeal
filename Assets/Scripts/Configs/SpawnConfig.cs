using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnConfig", menuName = "Configs/SpawnConfig")]
public class SpawnConfig : ScriptableObject
{
    [field:SerializeField] public int XSize { get; private set; }
    [field:SerializeField] public int YSize { get; private set; }
    
    [field:SerializeField] public float WidthCard { get; private set; }
    [field:SerializeField] public float HeightCard { get; private set; }
    
    [SerializeField] private GameObject[] _badCards;
    [SerializeField, Range(0, 100)] private int _badCardsChance;
    
    [SerializeField] private GameObject[] _goodCards;
    [SerializeField, Range(0, 100)] private int _goodCardsChance;
    
    [SerializeField] private GameObject[] _items;
    [SerializeField, Range(0, 100)] private int _itemsChance;

    public Dictionary<GameObject[], int> GetCardsChances()
    {
        return new Dictionary<GameObject[], int>
        {
            { _badCards, _badCardsChance },
            { _goodCards, _goodCardsChance },
            { _items, _itemsChance }
        };
    }
}
