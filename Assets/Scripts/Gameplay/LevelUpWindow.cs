using UnityEngine;
using TMPro;

public class LevelUpWindow : BaseWindow
{
    [SerializeField] private TMP_Text _currentBarbarianLevel;
    [SerializeField] private TMP_Text _currentAssasinLevel;
    [SerializeField] private TMP_Text _currentWarriorLevel;

    public override void Init()
    {
        _currentBarbarianLevel.text = PlayerSpawner.Instance.CharacterLevels.BarbarianLevel.ToString();
        _currentAssasinLevel.text = PlayerSpawner.Instance.CharacterLevels.AssasinLevel.ToString();
        _currentWarriorLevel.text = PlayerSpawner.Instance.CharacterLevels.WarriorLevel.ToString();
        EventBus.OnLevelUpWindowOpened?.Invoke();
    }

    private void StartNextBattle()
    {
        WindowsManager.Instance.CloseCurrentWindow();
        EventBus.OnBattleStart?.Invoke();
    }

    public void LevelUpBarbarian()
    {
        PlayerSpawner.Instance.LevelUpBarbarian();
        StartNextBattle();
    }

    public void LevelUpAssasin()
    {
        PlayerSpawner.Instance.LevelUpAssasin();
        StartNextBattle();
    }

    public void LevelUpWarrior()
    {
        PlayerSpawner.Instance.LevelUpWarrior();
        StartNextBattle();
    }
}