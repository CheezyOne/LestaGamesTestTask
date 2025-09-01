using UnityEngine;
using System.Collections;

public class DragonAttack : Attack
{
    [SerializeField] private int _flameAttackFrequency = 3;
    [SerializeField] private GameObject _flame;
    [SerializeField] private Transform _flameSpawn;
    [SerializeField] private int _additionalDamage;
    [SerializeField] private string _flameTrigger;
    [SerializeField] private float _flameAnimationTime;
    [SerializeField] private float _flameSpawnWait;

    private int _attackIndex;

    public override void AttackEnemy()
    {
        _attackIndex++;

        if (_attackIndex % _flameAttackFrequency == 0)
        {
            FlameAttack();
        }
        else
        {
            base.AttackEnemy();
        }
    }

    private void FlameAttack()
    {
        StartCoroutine(FlameSpawnRoutine());
        _animator.SetTrigger(_flameTrigger);

        if (AttackMissed())
        {
            return;
        }

        _enemyHealth.GetAttacked(_startingDamage + _additionalDamage + _characterInfo.Characteristics.Strength, _attackDelay);
    }

    private IEnumerator FlameSpawnRoutine()
    {
        yield return new WaitForSeconds(_flameSpawnWait);
        Destroy(Instantiate(_flame, _flameSpawn.position, _flameSpawn.rotation, _flameSpawn), _flameAnimationTime);
    }
}