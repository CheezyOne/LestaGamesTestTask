using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private string[] _attackTriggers;
    [SerializeField] private bool _sneakyAttack;
    [SerializeField] private Vector3 _missTextSpawnPosition;
    [SerializeField] private FloatingText _missText;
    [SerializeField] private string _missTextString;

    [SerializeField] protected int _startingDamage;
    [SerializeField] protected float _attackDelay;
    [SerializeField] protected Animator _animator;

    protected CharacterInfo _characterInfo;
    protected CharacterInfo _enemyInfo;
    protected int _calculatedDamage;
    protected Health _enemyHealth;

    public void SetEnemy(CharacterInfo enemy)
    {
        _enemyInfo = enemy;
        _enemyHealth = _enemyInfo.Health;
    }

    public virtual void AttackEnemy()
    {
        CalculateDamage();

        if (_attackTriggers.Length == 1)
        {
            _animator.SetTrigger(_attackTriggers[0]);
        }
        else
        {
            _animator.SetTrigger(_attackTriggers[Random.Range(0, _attackTriggers.Length)]);
        }

        if (AttackMissed())
        {
            return;
        }

        _enemyHealth.GetAttacked(_calculatedDamage, _attackDelay);
    }

    public void SetCharacterInfo(CharacterInfo characterInfo)
    {
        _characterInfo = characterInfo;
    }

    protected bool AttackMissed()
    {
        if (Random.Range(1, _characterInfo.Characteristics.Agility + _enemyInfo.Characteristics.Agility) < _enemyInfo.Characteristics.Agility)
        {
            Instantiate(_missText, _missTextSpawnPosition + transform.position, Quaternion.identity).SetText(_missTextString);
            return true;
        }

        return false;
    }

    protected virtual void CalculateDamage() 
    {
        _calculatedDamage = _startingDamage;
        _calculatedDamage += _characterInfo.Characteristics.Strength;

        if (_sneakyAttack)
        {
            if(_characterInfo.Characteristics.Agility > _enemyInfo.Characteristics.Agility)
            {
                _calculatedDamage++;
            }
        }
    }

    public virtual int GetStartingDamage()
    {
        return _startingDamage;
    }
}