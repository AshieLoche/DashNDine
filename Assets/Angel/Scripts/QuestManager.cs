using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class QuestManager : MonoBehaviour
{
    [Header("Quests Loader")]
    [SerializeField] private string csvLocation;
    [SerializeField] private int regionID;
    [SerializeField] QuestCSVLoader csvLoader;

    [Header("Quest Progress Tracker")]
    [SerializeField] private bool isInProgress = false;
    [SerializeField] private bool isIngredientsComplete = false;
    public DialogueManager dialogueManager;

    private List<QuestData> quests = new List<QuestData>();

    public static QuestManager Instance { get; private set; }


    //[SerializeField] private Sprite triangle;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        regionID = 1;
        quests = csvLoader.LoadCSV(csvLocation, regionID);
    }

    private void Update()
    {

    }

    public List<QuestData> GetQuests(int npcID)
    {
        List <QuestData> npcQuests = new List <QuestData>();
        foreach (var quest in quests)
        {
            if (quest.npcID == npcID)
                npcQuests.Add(quest);
        }
        return quests;
    }

}
