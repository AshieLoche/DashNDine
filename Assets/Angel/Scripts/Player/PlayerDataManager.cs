using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataManager : MonoBehaviour
{
    [Header("For Debugging Only")]
    [SerializeField] private bool triggerDefense = false;
    [Header("Player Data")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] GameObject player;
    private int playerHp;
    public int PlayerHp => playerHp;
    private int playerReputation;
    public int PlayerReputation => playerReputation;
    private int killedEnemies;
    public int KilledEnemies => killedEnemies;
    [SerializeField] AudioManager audioManager;

    [Header("Arena Data")]
    [SerializeField] ArenaData arenaData;
    [SerializeField] float ambushCDTime;
    private Vector2 posBeforeArena;

    [Header("Player Death")]
    [SerializeField] GameObject deathPanel;

    private void Start()
    {
        playerData.killedEnemies = 0;
        killedEnemies = 0;
        arenaData.enemyType = enemyType.Ambush;
        ambushCDTime = Random.Range(playerData.minAmbushCDTime, playerData.maxAmbushCDTime);
        playerData.ambushTimer = 0;
        playerData.inArena = false;

    }
    private void Update()
    {
        playerHp = playerData.currentHP;
        playerReputation = playerData.Reputation;
        killedEnemies = playerData.killedEnemies;

        if (playerHp == 0)
        {
            Debug.Log("Player Died");
            Die();
        }
        if (triggerDefense)
        {
            triggerDefense = false;
            CookFood();
        }
        if (playerData.Reputation > 100)
        {
            arenaData.difficulty = Difficulty.Normal;
        }
        else if (playerData.Reputation > 300)
        {
            arenaData.difficulty = Difficulty.Hard;
        }
    }
    public void ChangeHP(int hp)
    {
        playerData.currentHP = hp;  
    }
    public void AddReputation(int reputation)
    {
        playerData.Reputation += reputation;
    }
    public async void EnemyKilled()
    {
        playerData.killedEnemies++;
    }
    public void CookFood()
    {
        arenaData.enemyType = enemyType.Defense;
        playerData.inArena = true;
        audioManager.playAmbush();
        SceneManager.LoadScene("DefenseArena", LoadSceneMode.Additive);
    }
    public void DoneCooking()
    {
        playerData.inArena = false;
        arenaData.enemyType = enemyType.Ambush;
        audioManager.playNorm();
        SceneManager.LoadScene("DefenseArena", LoadSceneMode.Additive);
    }
    public void FailedCooking()
    {
        playerData.inArena = false;
        arenaData.enemyType = enemyType.Ambush;
        audioManager.playNorm();
        SceneManager.LoadScene("DefenseArena", LoadSceneMode.Additive);
    }
    public void DamagePlayer(int dmg)
    {
        playerData.currentHP = Mathf.Clamp(playerData.currentHP- dmg, 0, playerData.maxHP);
    }
    public void Die()
    {
        audioManager.playDeath();
        deathPanel.SetActive(true);
        //SceneManager.LoadScene("Game Over");
    }
    public void ResetAmbushTimer()
    {
        AddReputation(2);
        audioManager.playNorm();
        player.transform.position = posBeforeArena;
        playerData.inArena = false;
        playerData.killedEnemies = 0;
        playerData.ambushTimer = 0;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Combat Area" && arenaData.enemyType == enemyType.Ambush && !playerData.inArena)
        {
            playerData.ambushTimer = Mathf.Clamp(playerData.ambushTimer + Time.deltaTime, 0, ambushCDTime);
            if(playerData.ambushTimer == ambushCDTime)
            {
                posBeforeArena = player.transform.position;
                playerData.inArena = true;
                audioManager.playAmbush();
                SceneManager.LoadScene("DefenseArena", LoadSceneMode.Additive);
                player.transform.position = new Vector2(-18, -38);
            }
        }
    }
}
