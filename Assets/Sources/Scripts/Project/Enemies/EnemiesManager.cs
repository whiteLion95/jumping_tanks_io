using System.Collections.Generic;
using Deslab.Utils;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager Instance;
    [SerializeField] private RandomUserGenerator _randomUserGenerator;
    public List<Enemy> AllEnemies { get; private set; } = new List<Enemy>();
    [SerializeField] private RandomUser generatedUser;
    private void OnEnable()
    {
        Enemy.OnDie += OnEnemyDieHandler;
    }

    private void OnDisable()
    {
        Enemy.OnDie -= OnEnemyDieHandler;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void AddEnemyToList(Enemy enemy)
    {
        AllEnemies.Add(enemy);
        generatedUser = RandomUserGenerator.GetRandomUser();
        enemy.entityID = generatedUser.Username;
        RandomUserGenerator.SetUserbar(enemy.userBar, generatedUser);
    }

    private void OnEnemyDieHandler(Enemy diedEnemy)
    {
        Instance.AllEnemies.Remove(diedEnemy);

        if (Instance.AllEnemies.Count == 0)
            StaticManager.OnWin?.Invoke();
    }
}
