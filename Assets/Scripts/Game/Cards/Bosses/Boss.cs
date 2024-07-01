using UnityEngine;

public abstract class Boss : BadCard
{
    [SerializeField] protected int StartDamage;

    protected override void SetStartDamage()
    {
        ChangeDamage(StartDamage);
    }

    protected override void OnEnable()
    {
        MoveController.OnStep += PerformBossAction;
    }
    protected override void OnDisable()
    {
        //Player.AddFood(StartDamage);
        MoveController.OnStep -= PerformBossAction;
    }

    protected abstract void PerformBossAction();
}
