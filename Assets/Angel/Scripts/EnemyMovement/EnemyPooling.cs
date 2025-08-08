using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Vector3 enemyPoolBasePosition;
    [SerializeField] private int maxEnemyCount = 25;
    [SerializeField] private GameObject enemy;
    [SerializeField] private List<GameObject> standbyEnemies;
    private bool isInstantiating;

    void Start()
    {
        isInstantiating = true;
    }

    void Update()
    {
        if (isInstantiating)
        {
            GameObject spawnedEnemy = Instantiate(enemy, enemyPoolBasePosition, Quaternion.identity);
            standbyEnemies.Add(spawnedEnemy);
            spawnedEnemy.SetActive(false);
            if (standbyEnemies.Count == maxEnemyCount) isInstantiating = false;
        }
    }

    public List<GameObject> GetEnemies(int count)
    {
        List<GameObject> orderedEnemies = new List<GameObject>();
        for (int i = 0;  i < count; i++)
        {
            orderedEnemies.Add(standbyEnemies[i]);
        }
        return orderedEnemies;
    }

}
