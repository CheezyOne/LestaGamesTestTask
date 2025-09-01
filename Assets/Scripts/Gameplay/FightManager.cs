using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FightManager : Singleton<FightManager>
{
    [SerializeField] private int _fightsNumber;
    [SerializeField] private LoseWindow _loseWindow;
    [SerializeField] private FightVictoryWindow _fightVictoryWindow;
    [SerializeField] private GameVictoryWindow _gameVictoryWindow;
    [SerializeField] private TMP_Text _playerInfoText;
    [SerializeField] private TMP_Text _enemyInfoText;
    [SerializeField] private string _shieldDescription;
    [SerializeField] private string _stoneSkinDescription;
    [SerializeField] private string _slimeyDescription;

    private List<CharacterInfo> _opponents = new();
    private WeaponInfo _currentLoot;
    public WeaponInfo CurrentLoot => _currentLoot;
    private int _playerIndex = 0;
    private int _currentOpponentIndex;
    private int _readyOpponents = 0;
    private int _fightIndex;
    private bool _isFightEnded;

    public void AddOpponent(CharacterInfo opponentInfo)
    {
        _opponents.Add(opponentInfo);
    }

    private void OneOpponentReady()
    {
        _readyOpponents++;

        if (_readyOpponents == _opponents.Count)
            StartBattle();
    }

    private void StartBattle()
    {
        SetOpponentsInfo();
        _isFightEnded = false;
        int maxAgility = 0;

        for (int i = _opponents.Count - 1; i > 0; i--)
        {
            _opponents[i].Attack.SetEnemy(_opponents[i - 1]);
            _opponents[i].Health.SetOpponentsInfos(_opponents[i], _opponents[i - 1]);

            if (maxAgility < _opponents[i].Characteristics.Agility)
            {
                maxAgility = _opponents[i].Characteristics.Agility;
                _currentOpponentIndex = i;
            }
        }

        _opponents[0].Attack.SetEnemy(_opponents[_opponents.Count - 1]);
        _opponents[0].Health.SetOpponentsInfos(_opponents[0], _opponents[_opponents.Count - 1]);

        if (maxAgility < _opponents[0].Characteristics.Agility)
        {
            _currentOpponentIndex = 0;
        }

        if(maxAgility == _opponents[_playerIndex].Characteristics.Agility)
        {
            _currentOpponentIndex = _playerIndex;
        }

        NextMove();
    }

    private void SetOpponentsInfo()
    {
        SetPlayerInfo();
        SetEnemyInfo();
    }

    private void ClearOpponentsInfo()
    {
        _playerInfoText.text = "";
        _enemyInfoText.text = "";
    }

    private void SetPlayerInfo()
    {
        _playerInfoText.text = "Сила:" + _opponents[_playerIndex].Characteristics.Strength + '\n' +
            "Ловкость:" + _opponents[_playerIndex].Characteristics.Agility + '\n' +
            "Выносливость:" + _opponents[_playerIndex].Characteristics.Durability + '\n' +
            "Урон оружия:" + _opponents[_playerIndex].Attack.GetStartingDamage() + '\n';

        if (_opponents[_playerIndex].Health.HealthModificators.Contains(HealthModificator.StoneSkin))
            _playerInfoText.text += _stoneSkinDescription + '\n';
        if (_opponents[_playerIndex].Health.HealthModificators.Contains(HealthModificator.Shield))
            _playerInfoText.text += _shieldDescription + '\n';
    }

    private void SetEnemyInfo()
    {
        int enemyIndex = _opponents.Count - 1;
        _enemyInfoText.text = "Сила:" + _opponents[enemyIndex].Characteristics.Strength + '\n' +
            "Ловкость:" + _opponents[enemyIndex].Characteristics.Agility + '\n' +
            "Выносливость:" + _opponents[enemyIndex].Characteristics.Durability + '\n' +
            "Урон оружия:" + _opponents[enemyIndex].Attack.GetStartingDamage() + '\n';

        if (_opponents[enemyIndex].Health.HealthModificators.Contains(HealthModificator.StoneSkin))
            _enemyInfoText.text += _stoneSkinDescription + '\n';
        if (_opponents[enemyIndex].Health.HealthModificators.Contains(HealthModificator.Slimey))
            _enemyInfoText.text += _slimeyDescription + '\n';
    }

    private void EndFight()
    {
        _isFightEnded = true;
        ClearOpponentsInfo();
        _readyOpponents = 0;
    }

    private void DestroyOpponents()
    {
        for (int i = 0; i < _opponents.Count; i++)
        {
            Destroy(_opponents[i].gameObject);
        }

        _opponents.Clear();
    }

    private void OnPlayerDeath()
    {
        EndFight();
        WindowsManager.Instance.OpenWindow(_loseWindow);
    }

    private void OnEnemyDeath(WeaponInfo newWeaponInfo)
    {
        _currentLoot = newWeaponInfo;
        _fightIndex++;
        EndFight();

        if(_fightIndex == _fightsNumber)
        {
            WindowsManager.Instance.OpenWindow(_gameVictoryWindow);
        }
        else
        {
            WindowsManager.Instance.OpenWindow(_fightVictoryWindow);
        }
    }

    private void NextMove()
    {
        if (_isFightEnded)
            return;

        _opponents[_currentOpponentIndex].Attack.AttackEnemy();
        _currentOpponentIndex++;

        if (_currentOpponentIndex >= _opponents.Count)
            _currentOpponentIndex = 0;
    }

    private void OnEnable()
    {
        EventBus.OnWalkingComplete += OneOpponentReady;
        EventBus.OnAttackingComplete += NextMove;
        EventBus.OnPlayerDeath += OnPlayerDeath;
        EventBus.OnEnemyDeath += OnEnemyDeath;
        EventBus.OnLevelUpWindowOpened += DestroyOpponents;
    }

    private void OnDisable()
    {
        EventBus.OnWalkingComplete -= OneOpponentReady;
        EventBus.OnAttackingComplete -= NextMove;
        EventBus.OnPlayerDeath -= OnPlayerDeath;
        EventBus.OnEnemyDeath -= OnEnemyDeath;
        EventBus.OnLevelUpWindowOpened -= DestroyOpponents;
    }
}