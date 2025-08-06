using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;



public class QuestManager : MonoBehaviour
{
    [Header("Quest Collections")]
    [SerializeField] private List<QuestData> quests = new List<QuestData>();

    [Header("Quest Progress Tracker")]
    // Max 3 simultaneous quests *Ask First*
    [SerializeField] GameObject QuestPanel1;
    [SerializeField] GameObject QuestPanel2;
    [SerializeField] GameObject QuestPanel3;

    public List<QuestData> GetNPCQuests(int npcID)
    {
        List<QuestData> npcQuests = new List<QuestData>();
        foreach (QuestData quest in quests)
        {
            if(quest.npcID == npcID)
            npcQuests.Add(quest);
        }
        Debug.Log("Successfully Obtained Quests");
        return npcQuests;
    }

}
