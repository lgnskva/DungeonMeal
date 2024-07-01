using UnityEngine;

[CreateAssetMenu(menuName = "PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [field:SerializeField] public int MaxHealth { get; private set; }
    [field:SerializeField] public CharacterInfo[] Characters { get; private set; }
    [field:SerializeField] public string DefaultCharacterId { get; private set; }

}
