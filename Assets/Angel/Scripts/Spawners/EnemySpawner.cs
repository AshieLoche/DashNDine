using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector3 enemyBasePosition;
    [SerializeField] private List<Vector3> spawnPoints;
    [SerializeField] private int ambushEnemyCount, defEnemyCount;
    [SerializeField] List<GameObject> spawnedEnemies;
    private EnemyPool pool;

    public void SpawnEnemy(enemyType enemyType)
    {
        switch(enemyType)
        {
            case enemyType.Ambush:
                ambushEnemyCount = Random.Range(5, 10);
                spawnedEnemies = pool.GetEnemies(ambushEnemyCount);
                break;
            case enemyType.Defense:
                spawnedEnemies = pool.GetEnemies(defEnemyCount);
                break;
            default: break;

        }
    }
}
