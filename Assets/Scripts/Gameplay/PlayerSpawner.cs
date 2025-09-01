using UnityEngine;
using System;

public class PlayerSpawner : Singleton<PlayerSpawner>
{
    [SerializeField] private PlayerCharacter[] _playerCharacters;
    [SerializeField] private Transform _playerSpawn;
    [SerializeField] private Vector3 _playerRotation;
    [SerializeField] private int _healthPerBarbarianLevel;
    [SerializeField] private int _healthPerWarriorLevel;
    [SerializeField] private int _healthPerAssasinLevel;
    [SerializeField] private int _levelForAssasinAgilityUp = 2;
    [SerializeField] private int _levelForWarriorStrengthUp = 3;
    [SerializeField] private int _levelForBarbarianDurabilityUp = 3;

    private CharacterLevels _characterLevels;
    private WeaponInfo _currentWeaponInfo;
    private int _playerCharacterIndex;

    public WeaponInfo CurrentWeaponInfo => _currentWeaponInfo;
    public CharacterLevels CharacterLevels => _characterLevels;

    public void SetPlayerCharacterIndex(int index)
    {
        _playerCharacterIndex = index;
        _playerCharacters[_playerCharacterIndex].Characteristics.CreateRandomCharacteristics();
        _currentWeaponInfo = _playerCharacters[_playerCharacterIndex].StartWeaponInfo;

        switch (index)
        {
            case 0:
                {
                    LevelUpAssasin();
                    break;
                }
            case 1:
                {
                    LevelUpWarrior();
                    break;
                }
            case 2:
                {
                    LevelUpBarbarian();
                    break;
                }
        }
    }

    private void SpawnPlayer()
    {
        PlayerCharacterInfo player = Instantiate(_playerCharacters[_playerCharacterIndex].Character, _playerSpawn.position, Quaternion.Euler(_playerRotation));
        player.SetInfo(_playerCharacters[_playerCharacterIndex].Characteristics);
        player.SpawnWeapon(_currentWeaponInfo);
        player.SetLevels(_characterLevels);
        int playerHealth = _playerCharacters[_playerCharacterIndex].Characteristics.Durability;
        playerHealth *= (_characterLevels.BarbarianLevel + _characterLevels.WarriorLevel + _characterLevels.AssasinLevel);
        playerHealth += _characterLevels.BarbarianLevel * _healthPerBarbarianLevel;
        playerHealth += _characterLevels.WarriorLevel * _healthPerWarriorLevel;
        playerHealth += _characterLevels.AssasinLevel * _healthPerAssasinLevel;
        player.SetHealth(playerHealth);
        FightManager.Instance.AddOpponent(player);
    }

    public void SetNewWeapon(WeaponInfo weaponInfo)
    {
        _currentWeaponInfo = weaponInfo;
    }

    public void LevelUpBarbarian()
    {
        CharacterLevels newCharacterLevels = new()
        {
            AssasinLevel = _characterLevels.AssasinLevel,
            WarriorLevel = _characterLevels.WarriorLevel,
            BarbarianLevel = _characterLevels.BarbarianLevel + 1
        };

        if(newCharacterLevels.BarbarianLevel == _levelForBarbarianDurabilityUp)
        {
            _playerCharacters[_playerCharacterIndex].Characteristics.Durability++;
        }

        _characterLevels = newCharacterLevels;
    }

    public void LevelUpWarrior()
    {
        CharacterLevels newCharacterLevels = new()
        {
            AssasinLevel = _characterLevels.AssasinLevel,
            WarriorLevel = _characterLevels.WarriorLevel + 1,
            BarbarianLevel = _characterLevels.BarbarianLevel,
        };

        if (newCharacterLevels.WarriorLevel == _levelForWarriorStrengthUp)
        {
            _playerCharacters[_playerCharacterIndex].Characteristics.Strength++;
        }

        _characterLevels = newCharacterLevels;
    }

    public void LevelUpAssasin()
    {
        CharacterLevels newCharacterLevels = new()
        {
            AssasinLevel = _characterLevels.AssasinLevel + 1,
            WarriorLevel = _characterLevels.WarriorLevel,
            BarbarianLevel = _characterLevels.BarbarianLevel
        };

        if (newCharacterLevels.AssasinLevel == _levelForAssasinAgilityUp)
        {
            _playerCharacters[_playerCharacterIndex].Characteristics.Agility++;
        }

        _characterLevels = newCharacterLevels;
    }

    private void OnEnable()
    {
        EventBus.OnBattleStart += SpawnPlayer;
    }

    private void OnDisable()
    { 
        EventBus.OnBattleStart -= SpawnPlayer;
    }
}

[Serializable]
public class PlayerCharacter
{
    public WeaponInfo StartWeaponInfo;
    public PlayerCharacterInfo Character;
    public Characteristics Characteristics;
}