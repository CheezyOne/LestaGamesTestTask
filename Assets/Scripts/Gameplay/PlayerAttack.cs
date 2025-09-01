using UnityEngine;

public class PlayerAttack : Attack
{
    [SerializeField] private WeaponInfo _weaponInfo;
    [SerializeField] private int _crushingVsFragileMultiplicator = 2;
    [SerializeField] private int _choppingVsSlimeyMultiplicator = 0;
    [SerializeField] private int _assasinLevelForToxicAttack = 3;

    private PlayerCharacterInfo _playerCharacterInfo;
    private int _attackIndex = 0;

    public void SetWeaponInfo(WeaponInfo weaponInfo)
    {
        _weaponInfo = weaponInfo;
    }

    protected override void CalculateDamage()
    {
        _calculatedDamage = _weaponInfo.Damage;

        for (int i = 0; i < _enemyHealth.HealthModificators.Count; i++)
        {
            if (_enemyHealth.HealthModificators[i] == HealthModificator.Fragile)
            {
                if (_weaponInfo.WeaponType == WeaponType.Crushing)
                    _calculatedDamage *= _crushingVsFragileMultiplicator;
            }
            else if (_enemyHealth.HealthModificators[i] == HealthModificator.Slimey)
            {
                if (_weaponInfo.WeaponType == WeaponType.Chopping)
                    _calculatedDamage *= _choppingVsSlimeyMultiplicator;
            }
        }

        _calculatedDamage += _playerCharacterInfo.Characteristics.Strength;

        if (_playerCharacterInfo.CharacterLevels.WarriorLevel > 0 && _attackIndex == 0)
        {
            _calculatedDamage += _weaponInfo.Damage;
        }

        if (_playerCharacterInfo.CharacterLevels.AssasinLevel > 0 && _playerCharacterInfo.Characteristics.Agility > _enemyInfo.Characteristics.Agility)
        {
            _calculatedDamage++;
        }

        if (_playerCharacterInfo.CharacterLevels.BarbarianLevel > 0)
        {
            if (_attackIndex <= 2)
            {
                _calculatedDamage += 2;
            } 
            else
            {
                _calculatedDamage--;
            }
        }

        if (_playerCharacterInfo.CharacterLevels.AssasinLevel == _assasinLevelForToxicAttack)
        {
            _calculatedDamage += _attackIndex;
        }

        if (_calculatedDamage < 0)
            _calculatedDamage = 0;

        _attackIndex++;
    }

    public void SetCharacterInfo(PlayerCharacterInfo characterInfo)
    {
        _playerCharacterInfo = characterInfo;
    }

    public override int GetStartingDamage()
    {
        return _weaponInfo.Damage;
    }
}