using UnityEngine;
using TMPro;

public class FightVictoryWindow : BaseWindow
{
    [SerializeField] private LevelUpWindow _levelUpWindow;
    [SerializeField] private Transform _newWeaponPosition;
    [SerializeField] private TMP_Text _newWeaponDamageText;
    [SerializeField] private TMP_Text _newWeaponTypeText;
    [SerializeField] private Transform _oldWeaponPosition;
    [SerializeField] private TMP_Text _oldWeaponDamageText;
    [SerializeField] private TMP_Text _oldWeaponTypeText;
    [SerializeField] private string _piercingType;
    [SerializeField] private string _choppingType;
    [SerializeField] private string _crushingType;
    [SerializeField] private Vector3 _weaponScale;

    private WeaponInfo _newWeaponInfo;
    private WeaponInfo _oldWeaponInfo;

    public override void Init()
    {
        _newWeaponInfo = FightManager.Instance.CurrentLoot;
        _oldWeaponInfo = PlayerSpawner.Instance.CurrentWeaponInfo;
        _newWeaponDamageText.text = _newWeaponInfo.Damage.ToString();
        _oldWeaponDamageText.text = _oldWeaponInfo.Damage.ToString();
        Instantiate(_newWeaponInfo.Weapon, _newWeaponPosition.position, Quaternion.identity, _newWeaponPosition).transform.localScale = _weaponScale;
        Instantiate(_oldWeaponInfo.Weapon, _oldWeaponPosition.position, Quaternion.identity, _oldWeaponPosition).transform.localScale = _weaponScale;
        SetWeaponTypeText(_newWeaponTypeText, _newWeaponInfo);
        SetWeaponTypeText(_oldWeaponTypeText, _oldWeaponInfo);
    }

    private void SetWeaponTypeText(TMP_Text textComponent, WeaponInfo weaponInfo)
    {
        switch (weaponInfo.WeaponType)
        {
            case WeaponType.Peircing:
                {
                    textComponent.text = _piercingType;
                    break;
                }
            case WeaponType.Chopping:
                {
                    textComponent.text = _choppingType;
                    break;
                }
            case WeaponType.Crushing:
                {
                    textComponent.text = _crushingType;
                    break;
                }
        }
    }

    private void OpenLevelUpWindow()
    {
        WindowsManager.Instance.CloseCurrentWindow();
        WindowsManager.Instance.OpenWindow(_levelUpWindow);
    }

    public void OnChangeWeaponButton()
    {
        PlayerSpawner.Instance.SetNewWeapon(_newWeaponInfo);
        OpenLevelUpWindow();
    }

    public void OnKeepWeaponButton()
    {
        OpenLevelUpWindow();
    }
}