using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestCSVLoader: MonoBehaviour
{
    private List<QuestData> quests = new List<QuestData>();

    public List<QuestData> LoadCSV(string csvLocation, int regionID)
    {
        TextAsset csvFile = null;
        try
        {
            csvFile = Resources.Load<TextAsset>(csvLocation);

            StringReader reader = new StringReader(csvFile.text);

            bool firstLine = true;
            while (reader.Peek() > -1)
            {
                string line = reader.ReadLine();
                if (firstLine) { firstLine = false; continue; } // Skip header

                string[] values = line.Split(',');

                switch (regionID)
                {
                    case 1:

                        Region1QuestData item = new Region1QuestData
                        {
                            npcID = int.Parse(values[0]),
                            taskID = int.Parse(values[1]),
                            taskDialogue = values[2],
                            taskDescription = values[3],
                            reputation = int.Parse(values[4]),

                            requiredWheat = int.Parse(values[5]),
                            requiredHoney = int.Parse(values[6]),
                            requiredBerry = int.Parse(values[7]),
                            requiredTomato = int.Parse(values[8]),
                            requiredCarrot = int.Parse(values[9]),
                            requiredOnion = int.Parse(values[10])
                        };
                        quests.Add(item);
                        break;

                    case 2:
                        Region2QuestData quest = new Region2QuestData
                        {
                            npcID = int.Parse(values[0]),
                            taskID = int.Parse(values[1]),
                            taskDialogue = values[2],
                            taskDescription = values[3],
                            reputation = int.Parse(values[4]),

                            requiredWheat = int.Parse(values[5]),
                            requiredHoney = int.Parse(values[6]),
                            requiredBerry = int.Parse(values[7]),
                            requiredTomato = int.Parse(values[8]),
                            requiredCarrot = int.Parse(values[9]),
                            requiredOnion = int.Parse(values[10])
                        };
                        quests.Add(quest);
                        break;
                }

            }

            Debug.Log("Loaded " + quests.Count + " items from CSV.");
            foreach (var quest in quests)
            {
                Debug.Log(quest.taskID + ", " + quest.taskDialogue);
            }
            return quests;
        }
        catch
        {
            Debug.Log("CSV location does not exist");
            return null;   
        }


    }
}
