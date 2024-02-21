using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public abstract void DoAbility(int lvlAbility, Player player);
}
