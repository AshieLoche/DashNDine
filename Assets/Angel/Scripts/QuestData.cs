using UnityEngine;

public class QuestData
{
    public int npcID;
    public int taskID;
    public string taskDialogue;
    public string taskDescription;
    public int reputation;
}

public class Region1QuestData : QuestData
{
    public int requiredWheat, requiredHoney, requiredBerry, requiredTomato, requiredCarrot, requiredOnion;
}

public class Region2QuestData : QuestData
{
    public int requiredWheat, requiredHoney, requiredBerry, requiredTomato, requiredCarrot, requiredOnion;
}
