using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueCSVLoader : MonoBehaviour
{
    private List<DialogueData> dialogues = new List<DialogueData>();

    public List<DialogueData> LoadCSV(string csvLocation)
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

                DialogueData data = new DialogueData
                {
                    taskID = int.Parse(values[0]),
                    npcName = values[1],
                    acceptDialogue = values[2],
                    finishDialogue = values[3],
                    failedDialogue = values[4],
                    unfinishedDialogue = values[5]
                };

                dialogues.Add(data);

            }

            Debug.Log("Loaded " + dialogues.Count + " items from CSV.");
            foreach (var dialogue in dialogues)
            {
                Debug.Log(dialogue.taskID + ", " + dialogue.npcName);
            }
            return dialogues;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }


    }
}
