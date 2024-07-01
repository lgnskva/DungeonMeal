using System;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public static Action OnEndAbility;
    public abstract void DoAbility();
}
