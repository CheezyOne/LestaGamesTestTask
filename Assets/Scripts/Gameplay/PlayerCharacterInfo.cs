using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerAttack))]
public class PlayerCharacterInfo : CharacterInfo
{
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private GameObject[] _weapons;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private int _shieldWarriorLevel = 2;
    [SerializeField] private int _stoneSkinBarbarianLevel = 2;

    private CharacterLevels _characterLevels;

    public CharacterLevels CharacterLevels => _characterLevels;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _attack = GetComponent<Attack>();
        _playerAttack.SetCharacterInfo(this);
        _attack.SetCharacterInfo(this);
    }

    public void SetHealth(int health)
    {
        _playerHealth.SetHealth(health);
        List<HealthModificator> healthModificators = new();

        if (CharacterLevels.WarriorLevel >= _shieldWarriorLevel)
            healthModificators.Add(HealthModificator.Shield);
        if (CharacterLevels.BarbarianLevel >= _stoneSkinBarbarianLevel)
            healthModificators.Add(HealthModificator.StoneSkin);

        _playerHealth.SetModificators(healthModificators);
    }

    public void SetLevels(CharacterLevels characterLevels)
    {
        _characterLevels = characterLevels;
    }

    public void SetInfo(Characteristics characteristics)
    {
        _characteristics = characteristics;
    }

    public void SpawnWeapon(WeaponInfo weaponInfo)
    {
        for(int i = 0;i<_weapons.Length;i++)
        {
            if (_weapons[i].name == weaponInfo.Weapon.name)
            {
                _weapons[i].SetActive(true);
            }
        }

        _playerAttack.SetWeaponInfo(weaponInfo);
    }
}

public struct CharacterLevels
{
    public int AssasinLevel;
    public int WarriorLevel;
    public int BarbarianLevel;
}