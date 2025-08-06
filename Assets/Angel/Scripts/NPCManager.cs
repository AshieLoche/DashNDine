using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private NPCData npcData;
    [Header("NPC Information")]
    [SerializeField] private int region;
    [SerializeField] private int npcID;
    [SerializeField] private string npcName;
    [SerializeField] private Sprite image;
    [SerializeField] private int completedQuests;
    [SerializeField] private bool isInProgress;

    [SerializeField] private List<QuestData> quests;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        region = npcData.regionID;
        npcID = npcData.npcID;
        npcName = npcData.npcName;
        image = npcData.npcImage;
        completedQuests = npcData.completedQuests;

        quests = QuestManager.Instance.GetQuests(npcID);
    }

    public QuestData GiveQuest()
    {
        return quests[completedQuests];
    }
    public string GetName()
    {
        return npcName;
    }
    public Sprite GetImage()
    {
        return image;
    }
    public void AcceptQuest()
    {
        Debug.Log("Quest has been accepted");
        isInProgress = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entered");
        QuestManager.Instance.dialogueManager.OpenDialogue(this, quests[completedQuests]);
    }
}
