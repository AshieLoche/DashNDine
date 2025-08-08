using System.Collections;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Arena Information")]
public class ArenaInformation : ScriptableObject
{
    public enemyType enemyType;
    public Difficulty difficulty;
}

public class ArenaManager : MonoBehaviour
{
    [Header("Required Game Items")]
    [SerializeField] private ArenaInformation arenaData;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private int startTime = 3;
    private float countDownTime;

    private AmbushManager ambushManager;
    private PotDefenseManager potDefenseManager;

    [Header("Scene UI")]
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI countdownTxt;

    private void Awake()
    {
        if (ambushManager == null) gameObject.AddComponent<AmbushManager>();
        if (potDefenseManager == null) gameObject.AddComponent<PotDefenseManager>();
    }
    private void Start()
    {
        countDownTime = startTime;
        switch (arenaData.enemyType)
        {
            case enemyType.Ambush:
                enemySpawner.SpawnEnemy(enemyType.Ambush); 
                break;
            case enemyType.Defense:
                enemySpawner.SpawnEnemy(enemyType.Defense);
                break;
            default: 
                break;
        }
    }
    private void Update()
    {
        if (countDownTime > 0)
        {
            countDownTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            countDownTime = 0;
            TimerFinished();
        }
    }

    void UpdateTimerDisplay()
    {
        int displayTime = Mathf.CeilToInt(countDownTime);
        countdownTxt.text = displayTime.ToString();
    }

    void TimerFinished()
    {
        uiPanel.SetActive(false);
        Debug.Log("Countdown finished!");
    }

}
