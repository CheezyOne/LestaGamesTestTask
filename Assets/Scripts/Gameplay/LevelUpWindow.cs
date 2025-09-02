using UnityEngine;
using TMPro;

public class LevelUpWindow : BaseWindow
{
    [SerializeField] private TMP_Text _currentBarbarianLevel;
    [SerializeField] private TMP_Text _currentAssasinLevel;
    [SerializeField] private TMP_Text _currentWarriorLevel;
    [SerializeField] private GameObject _barbarianLevelUpButton;
    [SerializeField] private GameObject _assasinLevelUpButton;
    [SerializeField] private GameObject _warriorLevelUpButton;
    [SerializeField] private int _maxLevel;
    [SerializeField] private string _maxLevelText;

    public override void Init()
    {
        if(PlayerSpawner.Instance.CharacterLevels.BarbarianLevel == _maxLevel)
        {
            _currentBarbarianLevel.text = _maxLevelText;
            _barbarianLevelUpButton.SetActive(false);
        }
        else
        {
            _currentBarbarianLevel.text = PlayerSpawner.Instance.CharacterLevels.BarbarianLevel.ToString();
        }

        if (PlayerSpawner.Instance.CharacterLevels.AssasinLevel == _maxLevel)
        {
            _currentAssasinLevel.text = _maxLevelText;
            _assasinLevelUpButton.SetActive(false);
        }
        else
        {
            _currentAssasinLevel.text = PlayerSpawner.Instance.CharacterLevels.AssasinLevel.ToString();
        }

        if (PlayerSpawner.Instance.CharacterLevels.WarriorLevel == _maxLevel)
        {
            _currentWarriorLevel.text = _maxLevelText;
            _warriorLevelUpButton.SetActive(false);
        }
        else
        {
            _currentWarriorLevel.text = PlayerSpawner.Instance.CharacterLevels.WarriorLevel.ToString();
        }

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