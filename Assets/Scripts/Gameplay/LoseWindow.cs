using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseWindow : BaseWindow
{
    [SerializeField] private string _fightSceneName;

    public void OnRestartButton()
    {
        SceneManager.LoadScene(_fightSceneName);   
    }
}