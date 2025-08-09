using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataManager : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] private PlayerData playerData;
    private int playerHp;
    public int PlayerHp => playerHp;
    private int playerReputation;
    public int PlayerReputation => playerReputation;
    private int killedEnemies;
    public int KilledEnemies => killedEnemies;

    [Header("Arena Data")]
    [SerializeField] ArenaData arenaData;
    [SerializeField] float ambushCDTime;
    private Vector2 posBeforeArena;
    private void Start()
    {
        playerData.killedEnemies = 0;
        killedEnemies = 0;
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
    }
    public void ChangeHP(int hp)
    {
        playerData.currentHP = hp;  
    }
    public void ChangeReputation(int reputation)
    {
        playerData.Reputation = reputation;
    }
    public void EnemyKilled()
    {
        playerData.killedEnemies++;
    }
    public void DamagePlayer()
    {
        playerData.currentHP = Mathf.Clamp(playerData.currentHP- 1, 0, playerData.maxHP);
    }
    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene("Game Over");
    }
    public void ResetAmbushTimer()
    {
        transform.position = posBeforeArena;
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
                posBeforeArena = transform.position;
                playerData.inArena = true;
                SceneManager.LoadScene("DefenseArena", LoadSceneMode.Additive);
                transform.position = new Vector2(-18, -38);
            }
        }
    }
}
