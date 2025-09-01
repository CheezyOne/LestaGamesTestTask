using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] private CharacterCreationWindow _characterCreationWindow;

    private void Start()
    {
        WindowsManager.Instance.OpenWindow(_characterCreationWindow);
    }
}