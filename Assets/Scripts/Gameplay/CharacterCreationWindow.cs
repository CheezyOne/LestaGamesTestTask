public class CharacterCreationWindow : BaseWindow
{
    public void SelectCharacter(int index)
    {
        PlayerSpawner.Instance.SetPlayerCharacterIndex(index);
        EventBus.OnBattleStart?.Invoke();
        Close();
    }
}