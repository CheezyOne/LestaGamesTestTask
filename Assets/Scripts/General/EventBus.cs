using System;

public static class EventBus
{
    public static Action<WeaponInfo> OnEnemyDeath;
    public static Action OnBattleStart;
    public static Action OnPlayerDeath;
    public static Action OnWalkingComplete;
    public static Action OnAttackingComplete;
    public static Action OnLevelUpWindowOpened;
}