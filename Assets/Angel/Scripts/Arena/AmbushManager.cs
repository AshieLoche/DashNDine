using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AmbushManager : MonoBehaviour
{
    [Header("Debugger Only")]
    [SerializeField] bool start = false;

    [Header("Player")]
    [SerializeField] GameObject player;

    [Header("Enemies")]
    [SerializeField] private int remainingEnemies;
    private EnemySpawner spawner;
    [SerializeField] private float minDelay = 1f;
    [SerializeField] private float maxDelay = 3f;

    [Header("Arena UI")]
    [SerializeField] private TextMeshProUGUI remainingEnemiesTxt;
    [SerializeField] private TextMeshProUGUI specialIngredientTxt;

    private bool isOngoing = false;

    private void Start()
    {
        spawner = GetComponent<EnemySpawner>();

        start = false;
    }
    public void StartDefense()
    {
        isOngoing = true;
        Instantiate(player);
        spawner.SpawnEnemy(enemyType.Ambush);

        if (spawner.IsDoneSpawning)
        {
            StartCoroutine(SummonEnemies());
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (start && !isOngoing)
        {
            StartDefense();
        }

        if (isOngoing)
        {
            remainingEnemies = 0;
            foreach (var e in spawner.SpawnedEnemies)
            {
                if (e.activeSelf)
                    remainingEnemies++;
            }
            remainingEnemiesTxt.text = remainingEnemies.ToString();

            if(remainingEnemies == 0)
            {
                player.GetComponent<PlayerDataManager>().ResetAmbushTimer();
                if (SceneManager.GetSceneByName("DefenseArena").isLoaded)
                {
                    SceneManager.UnloadSceneAsync("DefenseArena");
                }
            }
        }
    }
    IEnumerator SummonEnemies()
    {
        foreach (var e in spawner.SpawnedEnemies)
        {
            e.GetComponent<EnemyManager>().SetEnemyType(enemyType.Defense);
            e.SetActive(true);
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            e.GetComponent<EnemyManager>().MoveEnemy(player);
        }
    }
}
