using System;
using System.Collections.Generic;
using System.IO;
using DashNDine.EnumSystem;
using DashNDine.MiscSystem;
using DashNDine.ScriptableObjectSystem;
using UnityEngine;

namespace DashNDine.CoreSystem
{
    public class SaveManager : SingletonBehaviour<SaveManager>
    {
        // [SerializeField] private PlayerSO _playerSO;
        // [SerializeField] private QuestListSO _questListSO;
        // private readonly static string _saveFolderPath = Application.dataPath + "/CSVs/SaveDatas/";
        // private readonly string _playerSaveDataFilePath = _saveFolderPath + "PlayerSaveData.csv";
        // private readonly string _questSaveDataFilePath = _saveFolderPath + "QuestSaveData.csv";

        // protected override void Awake()
        // {
        //     base.Awake();

        //     _questListSO.ResetQuestObjectList();
        // }

        // public void Save()
        // {
        //     List<List<string>> playerSaveDataArray = new List<List<string>>
        //     {
        //         new List<string> { "Player ID", "Player Name", "Player Reputation Amount" },
        //         new List<string> { _playerSO.ID.ToString(), _playerSO.Name, _playerSO.ReputationAmount.ToString() },
        //     };

        //     List<List<string>> questSaveDataList = new List<List<string>>
        //     {
        //         new List<string> { "Quest ID", "Quest Objective", "Quest Status" },
        //     };

        //     foreach (QuestSO questSO in _questListSO.SOList)
        //     {
        //         questSaveDataList.Add(new List<string> {
        //             questSO.ID.ToString(),
        //             ConvertQuestObjectiveListToString(questSO.GetQuestObjectiveList()),
        //             questSO.GetStatus().ToString()
        //         });
        //     }

        //     WriteCSV(playerSaveDataArray, _playerSaveDataFilePath);
        //     WriteCSV(questSaveDataList, _questSaveDataFilePath);
        // }

        // private string ConvertQuestObjectiveListToString(IngredientStackListSO ingredientStackListSO)
        // {
        //     string questObjectiveString = "";
        //     for (int i = 0; i < ingredientStackListSO.Count; i++)
        //     {
        //         if (i > 0 && i < ingredientStackListSO.Count)
        //             questObjectiveString += "|";

        //         IngredientStackSO ingredientStackSO = ingredientStackListSO[i];
        //         questObjectiveString += $"{ingredientStackSO.GetIngredientSO().ID}_{ingredientStackSO.GetAmount()}";
        //     }
        //     return questObjectiveString;
        // }

        // private void WriteCSV(List<List<string>> data, string filePath)
        // {
        //     using (StreamWriter writer = new StreamWriter(filePath))
        //     {
        //         int rows = data.Count;
        //         int cols = data[0].Count;

        //         for (int i = 0; i < rows; i++)
        //         {
        //             string line = "";
        //             for (int j = 0; j < cols; j++)
        //             {
        //                 line += data[i][j];

        //                 // Add comma unless it's the last item
        //                 if (j < cols - 1)
        //                     line += ",";
        //             }
        //             writer.WriteLine(line);
        //         }
        //     }

        //     Debug.Log("CSV saved to: " + filePath);
        // }

        // public void Load()
        // {
        //     ReadCSV(ref _playerSO, _playerSaveDataFilePath);
        //     ReadCSV<QuestSO, QuestListSO>(ref _questListSO, _questSaveDataFilePath);
        // }

        // private void ReadCSV<TBaseSO, TListSO>(ref TListSO listSO, string filePath) where TBaseSO : BaseSO where TListSO : BaseListSO<TBaseSO>
        // {
        //     StreamReader reader = ReadCSV(filePath);

        //     while (!reader.EndOfStream)
        //     {
        //         string[] values = reader.ReadLine().Split(',');

        //         UpdateListSO<TBaseSO, TListSO>(ref listSO, values);
        //     }
        // }

        // private void ReadCSV<TBaseSO>(ref TBaseSO so, string filePath) where TBaseSO : BaseSO
        // {
        //     StreamReader reader = ReadCSV(filePath);

        //     while (!reader.EndOfStream)
        //     {
        //         string[] values = reader.ReadLine().Split(',');

        //         UpdateBaseSO(ref so, values);
        //     }
        // }

        // private StreamReader ReadCSV(string filePath)
        // {
        //     if (!File.Exists(filePath))
        //     {
        //         Debug.LogError($"{filePath} doest not exist.");
        //         return null;
        //     }

        //     using StreamReader reader = new(filePath);

        //     reader.ReadLine();

        //     return reader;
        // }

        // private void UpdateListSO<TBaseSO, TListSO>(ref TListSO listSO, string[] values) where TBaseSO : BaseSO where TListSO : BaseListSO<TBaseSO>
        // {
        //     switch (listSO)
        //     {
        //         case QuestListSO questListSO:
        //             if (!int.TryParse(values[0], out int questID))
        //                 return;

        //             QuestSO questSO = listSO.GetSOByID(questID) as QuestSO;
        //             UpdateBaseSO(ref questSO, values);
        //             questListSO.SOList[questID] = questSO;
        //             listSO = questListSO as TListSO;
        //             break;
        //     }
        // }

        // private void UpdateBaseSO<TBaseSO>(ref TBaseSO baseSO, string[] values, int id = -1) where TBaseSO : BaseSO
        // {
        //     switch (baseSO)
        //     {
        //         case PlayerSO playerSO:
        //             if (int.TryParse(values[0], out int playerID))
        //                 playerSO.ID = playerID;
        //             playerSO.Name = values[1];
        //             if (int.TryParse(values[2], out int playerReputationAmount))
        //                 playerSO.ReputationAmount = playerReputationAmount;
        //             baseSO = playerSO as TBaseSO;
        //             break;
        //         case QuestSO questSO:
        //             questSO.ID = id;
        //             questSO.QuestObjectiveList = ConvertStringToQuestObjectiveList(questSO.GetQuestObjectiveList(), values[1]);
        //             if (Enum.TryParse(values[2], out QuestStatus questStatus))
        //                 questSO.SetStatus(questStatus);
        //             break;
        //     }
        // }
        
        // private IngredientStackListSO ConvertStringToQuestObjectiveList(IngredientStackListSO questObjectiveList, string value)
        // {
        //     foreach (string questObjectiveJoined in value.Split("|"))
        //     {
        //         string[] questObjectSeparated = questObjectiveJoined.Split("_");

        //         if (int.TryParse(questObjectSeparated[0], out int ingredientID))
        //         {
        //             int index = questObjectiveList.FindIndex(qo => qo.GetIngredientSO().ID == ingredientID);

        //             if (int.TryParse(questObjectSeparated[1], out int collectedAmount))
        //             {
        //                 QuestObjective questObjective = questObjectiveList[index];

        //                 questObjective.CollectedAmount = collectedAmount;

        //                 questObjectiveList[index] = questObjective;
        //             }
        //         }
        //     }

        //     return questObjectiveList;
        // }
    }
}