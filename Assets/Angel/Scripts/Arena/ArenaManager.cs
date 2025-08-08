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
    [Header("Prerequisites")]
    [SerializeField] private ArenaInformation arenaData;
    private AmbushManager ambushManager;
    private PotDefenseManager potDefenseManager;

    [Header("Start Countdown")]
    [SerializeField] private int startTime = 3;
    private float countDownTime;
    private bool canStart = false, isInCombat = false;

    [Header("Scene UI")]
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private GameObject ambushPanel, defensePanel;   
    [SerializeField] private TextMeshProUGUI countdownTxt;

    private void Awake()
    {
        ambushManager = GetComponent<AmbushManager>();
        potDefenseManager = GetComponent<PotDefenseManager>();
    }
    private void Start()
    {
        countDownTime = startTime;
    }
    private void Update()
    {
        if (countDownTime > 0 && !canStart)
        {
            countDownTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else if (countDownTime <= 0 &&  !canStart)
        {
            countDownTime = 0;
            TimerFinished();
        }

        if(canStart && !isInCombat)
        {
            isInCombat = true;
            switch (arenaData.enemyType)
            {
                case enemyType.Ambush:
                    ambushManager.StartDefense(arenaData.difficulty);
                    break;
                case enemyType.Defense:
                    potDefenseManager.StartPotDefense(arenaData.difficulty);
                    break;
                default:
                    break;
            }
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
        switch (arenaData.enemyType)
        {
            case enemyType.Ambush:
                ambushPanel.SetActive(true);
                break;
            case enemyType.Defense:
                defensePanel.SetActive(true);
                break;
            default:
                break;
        }

        Debug.Log("Countdown finished!");
        canStart = true;
    }

}
