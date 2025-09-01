using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<CharacterInfo> _enemies;

    private void SpawnEnemy()
    {
        int randomInt = Random.Range(0, _enemies.Count);
        _enemies[randomInt].gameObject.SetActive(true);
        FightManager.Instance.AddOpponent(_enemies[randomInt]);
        _enemies.RemoveAt(randomInt);
    }

    private void OnEnable()
    {
        EventBus.OnBattleStart += SpawnEnemy;
    }

    private void OnDisable()
    {
        EventBus.OnBattleStart -= SpawnEnemy;
    }
}