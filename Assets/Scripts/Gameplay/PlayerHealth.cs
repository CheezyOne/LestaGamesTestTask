using System.Collections.Generic;

public class PlayerHealth : Health
{
    protected override void OnDie()
    {
        EventBus.OnPlayerDeath?.Invoke();
    }

    public void SetHealth(int health)
    {
        _maxHealth = health;
        SetStartHealth();
    }

    public void SetModificators(List <HealthModificator> healthModificators)
    {
        _healthModificators = healthModificators;
    }
}