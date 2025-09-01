using UnityEngine.SceneManagement;
using UnityEngine;

public class GameVictoryWindow : BaseWindow
{
    [SerializeField] private string _sceneName;

    public void RestartGame()
    {
        SceneManager.LoadScene(_sceneName);
    }
}