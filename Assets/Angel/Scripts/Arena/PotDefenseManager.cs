using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PotDefenseManager : MonoBehaviour
{
    [Header("Debugger Only")]
    [SerializeField] bool start = false;
    [Header("Pot")]
    [SerializeField] private GameObject pot;
    [SerializeField] private Vector2 potSpawnPt;
    private PotManager potManager;

    [Header("Enemies")]
    [SerializeField] private int remainingEnemies = 0;
    private EnemySpawner spawner;
    [SerializeField] private float minDelay = 1f;
    [SerializeField] private float maxDelay = 3f;

    [Header("Player Kill")]
    [SerializeField] private PlayerData playerData;
    private int killCount;

    [Header("Arena UI")]
    [SerializeField] private TextMeshProUGUI remainingEnemiesTxt;
    [SerializeField] private TextMeshProUGUI potHpTxt;
    [SerializeField] private TextMeshProUGUI specialIngredientTxt;

    private bool isOngoing = false;

    private void Start()
    {
        spawner = GetComponent<EnemySpawner>();
        potManager = pot.GetComponent<PotManager>();

        start = false;
    }

    public void StartPotDefense(Difficulty difficulty)
    {
        isOngoing = true;

        SummonPot();
        spawner.SpawnEnemy(enemyType.Defense);
        remainingEnemies = spawner.EnemyCount - playerData.killedEnemies;
        if (spawner.IsDoneSpawning)
        {
            StartCoroutine(SummonEnemies(difficulty));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(start && !isOngoing)
        {
            StartPotDefense(Difficulty.Easy);
        }

        if (isOngoing)
        {
            foreach(var e in spawner.SpawnedEnemies)
            {
                if(e.activeSelf) e.GetComponent<EnemyManager>().MoveEnemy(pot);
            }
            killCount = playerData.killedEnemies;
            remainingEnemies = spawner.EnemyCount - killCount;
            remainingEnemiesTxt.text = remainingEnemies.ToString();
            potHpTxt.text = potManager.CurrentHP.ToString();

            if (remainingEnemies == 0)
                PotDefenseSuccesful();
        }
    }

    public void SummonPot()
    {
        if (pot == null)
        {
            Instantiate(pot, potSpawnPt, Quaternion.identity);
        }
        pot.SetActive(true);
    }
    void PotDefenseSuccesful()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDataManager>().DoneCooking();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDataManager>().DoneCooking();
        Debug.Log("Defense Successful!");
        isOngoing = false;
        if (SceneManager.GetSceneByName("DefenseArena").isLoaded)
        {
            SceneManager.UnloadSceneAsync("DefenseArena");
        }
    }
    public void PotDestroyed()
    {
        Debug.Log("Pot has been destroyed. Defense failed...");
        isOngoing = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDataManager>().FailedCooking();
        if (SceneManager.GetSceneByName("DefenseArena").isLoaded)
        {
            SceneManager.UnloadSceneAsync("DefenseArena");
        }
    }

    IEnumerator SummonEnemies(Difficulty diff)
    {
        foreach (var e in spawner.SpawnedEnemies)
        {
            e.GetComponent<EnemyManager>().SetEnemyType(enemyType.Defense);
            e.GetComponent<EnemyManager>().SetDifficulty(diff);
            e.SetActive(true);
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

        }
    }
}
