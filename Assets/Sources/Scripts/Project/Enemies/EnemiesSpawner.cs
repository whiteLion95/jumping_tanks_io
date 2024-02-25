using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Enemy spawnedEnemy = Instantiate(enemyToSpawn, transform.position, transform.rotation, transform);
        EnemiesManager.Instance.AddEnemyToList(spawnedEnemy);
    }
}
