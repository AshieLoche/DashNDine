using System.Collections;
using TMPro;
using UnityEngine;

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

    public void StartPotDefense()
    {
        isOngoing = true;

        SummonPot();
        spawner.SpawnEnemy(enemyType.Defense);
        remainingEnemies = spawner.EnemyCount - playerData.killedEnemies;
        if (spawner.IsDoneSpawning)
        {
            StartCoroutine(SummonEnemies());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(start && !isOngoing)
        {
            StartPotDefense();
        }

        if (isOngoing)
        {
            killCount = playerData.killedEnemies;
            remainingEnemies = spawner.EnemyCount - killCount;
            remainingEnemiesTxt.text = remainingEnemies.ToString();
            potHpTxt.text = potManager.CurrentHP.ToString();
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
    public void PotDestroyed()
    {
        Debug.Log("Pot has been destroyed. Defense failed...");
        isOngoing = false;
    }

    IEnumerator SummonEnemies()
    {
        foreach (var e in spawner.SpawnedEnemies)
        {
            e.GetComponent<EnemyManager>().SetEnemyType(enemyType.Defense);
            e.SetActive(true);
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            e.GetComponent<EnemyManager>().MoveEnemy(pot);
        }
    }
}
