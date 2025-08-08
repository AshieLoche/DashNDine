using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector3 enemyBasePosition;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private int ambushEnemyCount, defEnemyCount;
    [SerializeField] List<GameObject> spawnedEnemies;
    public List<GameObject> SpawnedEnemies => spawnedEnemies;
    private EnemyPool pool;
    private bool isDoneSpawning = false;
    public bool IsDoneSpawning => isDoneSpawning;

    private void Start()
    {
       spawnedEnemies = new List<GameObject>();
       pool = GetComponent<EnemyPool>();
    }
    public void SpawnEnemy(enemyType enemyType)
    {
        isDoneSpawning = false;
        switch(enemyType)
        {
            case enemyType.Ambush:
                ambushEnemyCount = Random.Range(5, 10);
                spawnedEnemies = pool.GetEnemies(ambushEnemyCount);
                foreach(var e in spawnedEnemies)
                {
                    e.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count - 1)].position;
                }
                break;

            case enemyType.Defense:
                spawnedEnemies = pool.GetEnemies(defEnemyCount);
                foreach (var e in spawnedEnemies)
                {
                    e.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count - 1)].position;
                }
                break;
            default: break;
        }
        isDoneSpawning = true;
    }
}
