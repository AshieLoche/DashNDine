using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DashNDine.ClassSystem;
using DashNDine.EnumSystem;
using DashNDine.MiscSystem;
using DashNDine.ScriptableObjectSystem;
using DashNDine.StructSystem;
using UnityEditor;
using UnityEngine;

namespace DashNDine.EditorSystem
{
    public class CSVToSOConverter : EditorWindow
    {
        public enum CSVType
        {
            Regions,
            Ingredients,
            IngredientStacks,
            NPCs,
            Quests,
            Monsters
        }

        private Action<CSVType> _onConfirm;
        private CSVType _csvType = CSVType.Regions;

        private static void ShowWindow(Action<CSVType> onConfirm)
        {
            CSVToSOConverter window = GetWindow<CSVToSOConverter>("CSV to SO Converter");
            window.minSize = new Vector2(300, 50);
            window._onConfirm = onConfirm;
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("CSV", EditorStyles.boldLabel);
            _csvType = (CSVType)EditorGUILayout.EnumPopup("CSV Type", _csvType);

            if (GUILayout.Button("Confirm")) _onConfirm?.Invoke(_csvType);
            else if (GUILayout.Button("Cancel")) Close();
        }

        [MenuItem("Tools/Dash N Dine/CSV to SO Converter")]
        public static void ShowConverterWindow()
            => ShowWindow(onConfirm: ConvertCSVToSO);

        private static void ConvertCSVToSO(CSVType csvType)
        {
            string csvFilePath = $"Assets/CSVs/{csvType}.csv";
            string soFolderPath = $"Assets/SOs";

            if (csvType == CSVType.IngredientStacks)
            {
                // Save Inventory
                TrySaveInventorySO(soFolderPath);
                return;
            }


            using StreamReader reader = new StreamReader(csvFilePath, Encoding.UTF8);

            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                string[] values = reader.ReadLine().Split(',');

                CreateSO(values, csvType, soFolderPath);
            }

            AssetDatabase.SaveAssets();
        }

        private static void CreateSO(string[] values, CSVType csvType, string soFolderPath)
        {
            switch (csvType)
            {
                case CSVType.Regions:
                    // Retrieve RegionSO
                    RegionSO regionSO = TryReplaceBaseSOMainData<RegionSO>(csvType, values, soFolderPath);

                    // Replace RegionSO Description
                    regionSO.Description = TryReplaceString(regionSO.Description, values[2]);

                    // Repalce RegionSO MonsterDifficulty;
                    regionSO.MonsterDifficulty = TryReplaceFlagEnum(regionSO.MonsterDifficulty, values[3]);

                    // Replace RegionSO ReputationRequired
                    regionSO.ReputationRequired = TryReplaceInt(regionSO.ReputationRequired, values[4]);

                    // Replace RegionSO RegionStatus
                    regionSO.RegionStatus = RegionStatus.Locked;

                    // Save RegionSO
                    SaveSO(regionSO);

                    // Save RegionListSO
                    TrySaveListSO<RegionSO, RegionListSO>(regionSO, csvType, soFolderPath);
                    break;
                case CSVType.Ingredients:
                    // TODO: REMOVE ONCE OTHER REGIONS ARE IMPLEMENTED
                    if (values[2] != "1")
                        return;

                    // Retrieve IngredientSO
                    IngredientSO ingredientSO = TryReplaceBaseSOMainData<IngredientSO>(csvType, values, soFolderPath);

                    // Replace IngredientSO RegionSO
                    ingredientSO.RegionSO = TryReplaceSO<RegionSO, RegionListSO>(ingredientSO.RegionSO, CSVType.Regions, values[2], soFolderPath);

                    // Replace IngredientSO SpawnLocation
                    ingredientSO.SpawnLocation = TryReplaceEnum(ingredientSO.SpawnLocation, values[3]);

                    // Replace IngredientSO IngredientType
                    ingredientSO.IngredientType = TryReplaceEnum(ingredientSO.IngredientType, values[4]);

                    // Replace IngredientSO PotDefenseEffect
                    ingredientSO.PotDefenseEffect = TryReplaceString(ingredientSO.PotDefenseEffect, values[5]);

                    // Replace IngredientSO Sprite
                    ingredientSO.Sprite = TryReplaceSprite(ingredientSO.Sprite, csvType, ingredientSO.name);

                    // Replace IngredientSO PrefabTransform;
                    ingredientSO.PrefabTransform = TryReplacePrefabTransform(ingredientSO.PrefabTransform, csvType, ingredientSO.name);

                    // Save IngredientSO
                    SaveSO(ingredientSO);

                    // Save IngredientListSO
                    TrySaveListSO<IngredientSO, IngredientListSO>(ingredientSO, csvType, soFolderPath);
                    break;
                case CSVType.NPCs:
                    // TODO: REMOVE ONCE OTHER REGIONS ARE IMPLEMENTED
                    if (values[2] != "1")
                        return;
                        
                    // Retrieve NPCSO
                    NPCSO npcSO = TryReplaceBaseSOMainData<NPCSO>(csvType, values, soFolderPath);

                    // Replace NPCSO RegionSO
                    npcSO.RegionSO = TryReplaceSO<RegionSO, RegionListSO>(npcSO.RegionSO, CSVType.Regions, values[2], soFolderPath);

                    // Replace NPCSO Sprite Head
                    npcSO.spriteHead = TryReplaceSpriteHead(npcSO.sprite, csvType, npcSO.name);

                    // Replace NPCSO Sprite
                    npcSO.sprite = TryReplaceSprite(npcSO.sprite, csvType, npcSO.name);

                    // Replace NPCSO Prefab Transform;
                    npcSO.PrefabTransform = TryReplacePrefabTransform(npcSO.PrefabTransform, csvType, npcSO.name);

                    // Save NPCSO
                    SaveSO(npcSO);

                    // Save NPCListSO
                    TrySaveListSO<NPCSO, NPCListSO>(npcSO, csvType, soFolderPath);
                    break;
                case CSVType.Quests:
                    // TODO: REMOVE ONCE OTHER REGIONS ARE IMPLEMENTED
                    if (values[3] != "1")
                        return;

                    // Retrieve QuestSO
                    QuestSO questSO = TryReplaceBaseSOMainData<QuestSO>(csvType, values, soFolderPath);

                    // Replace QuestSO NPCSO
                    questSO.NPCSO = TryReplaceSO<NPCSO, NPCListSO>(questSO.NPCSO, CSVType.NPCs, values[2], soFolderPath);

                    // Replace QuestSO RegionSO;
                    questSO.RegionSO = TryReplaceSO<RegionSO, RegionListSO>(questSO.RegionSO, CSVType.Regions, values[3], soFolderPath);

                    // Replace QuestSO Description;
                    questSO.Description = TryReplaceString(questSO.Description, values[4]);

                    // Replace QuestSO QuestObjectiveList
                    questSO.QuestObjectiveList = TryReplaceQuestObjectiveList(questSO.name, values[5], soFolderPath);

                    // Replace QuestSO QuestType
                    questSO.QuestType = TryReplaceEnum(questSO.QuestType, values[6]);

                    // Replace QuestSO MonsterCount
                    questSO.MonsterCount = TryReplaceInt(questSO.MonsterCount, values[7]);

                    // Replace QuestSO ReputationRequired
                    questSO.ReputationRequired = TryReplaceInt(questSO.ReputationRequired, values[8]);

                    // Replace QuestSO Reward
                    questSO.Reward = TryReplaceInt(questSO.Reward, values[9]);

                    // Replace QuestSO BonusReward
                    questSO.BonusReward = TryReplaceInt(questSO.BonusReward, values[10]);

                    // Replace QuestSO Prompt
                    questSO.Prompt = TryReplaceString(questSO.Prompt, values[11]);

                    // Replace QuestSO Waiting
                    questSO.Waiting = TryReplaceString(questSO.Waiting, values[12]);

                    // Replace QuestSO Success
                    questSO.Success = TryReplaceString(questSO.Success, values[13]);

                    // Replace QuestSO Failure
                    questSO.Failure = TryReplaceString(questSO.Failure, values[14]);

                    // Replace QuestSO QuestStatus
                    questSO.Lock();
                    
                    // Replace QuestSO HasCookedSuccessfully
                    questSO.CookFailed();

                    // Save QuestSO
                    SaveSO(questSO);

                    // Save QuestListSO
                    TrySaveListSO<QuestSO, QuestListSO>(questSO, csvType, soFolderPath);
                    break;
                case CSVType.Monsters:
                    // Retrieve MonsterSO
                    MonsterSO monsterSO = TryReplaceBaseSOMainData<MonsterSO>(csvType, values, soFolderPath);

                    // Replace MonsterSO MonsterQTEList
                    monsterSO.MonsterQTEList = TryReplaceMonsterQTEStructList(values[2]);

                    // Replace MonsterSO Note
                    monsterSO.Note = TryReplaceString(monsterSO.Note, values[3]);

                    // Save MonsterSO
                    SaveSO(monsterSO);

                    // Save MonsterListSO
                    TrySaveListSO<MonsterSO, MonsterListSO>(monsterSO, csvType, soFolderPath);
                    break;
                default:
                    break;
            }
        }

        #region Retrive SO
        private static TListSO TryGetListSO<TBaseSO, TListSO>(CSVType csvType, string soFolderPath) where TBaseSO : BaseSO where TListSO : BaseListSO<TBaseSO>
        {
            string listSOFileName = csvType.ToString()[..^1] + "ListSO";
            return TryGetSO<TListSO>(csvType, listSOFileName, soFolderPath);
        }

        private static TBaseSO TryGetBaseSO<TBaseSO>(CSVType csvType, string value, string soFolderPath, out string newSOName) where TBaseSO : BaseSO
        {
            newSOName = Utils.RemoveWhiteSpaceAndDash(value) + "SO";
            return TryGetSO<TBaseSO>(csvType, newSOName, soFolderPath);
        }

        private static TSO TryGetSO<TSO>(CSVType csvType, string soName, string soFolderPath) where TSO : ScriptableObject
        {
            string soFilePath = soFolderPath + $"/{csvType}/" + soName + ".asset";

            return LoadOrCreateSO<TSO>(soFilePath);
        }

        private static TSO LoadOrCreateSO<TSO>(string path) where TSO : ScriptableObject
        {
            if (AssetDatabase.AssetPathExists(path))
                return AssetDatabase.LoadAssetAtPath<TSO>(path);
            else
            {
                TSO so = CreateInstance<TSO>();
                AssetDatabase.CreateAsset(so, path);
                return so;
            }
        }
        #endregion

        #region Replace Data
        private static TBaseSO TryReplaceBaseSOMainData<TBaseSO>(CSVType csvType, string[] values, string soFolderPath) where TBaseSO : BaseSO
        {
            // Retrieve SO
            TBaseSO so = TryGetBaseSO<TBaseSO>(csvType, values[1], soFolderPath, out string newSOFileName);

            // Replace SO File Name
            so.name = TryReplaceString(so.name, newSOFileName);

            // Replace SO ID
            so.ID = TryReplaceInt(so.ID, values[0]);

            // Replace SO Name
            so.Name = TryReplaceString(so.Name, values[1]);

            return so;
        }

        private static string TryReplaceString(string oldString, string newString)
        {
            newString = newString.Replace('|', ',');
            return (oldString != newString) ? newString : oldString;
        }

        private static int TryReplaceInt(int oldInt, string newString)
        {
            if (Utils.TryConvertStringToInt(newString, out int newInt))
                return (oldInt != newInt) ? newInt : oldInt;

            Debug.Log($"Cannot convert {newString} to int!");
            return oldInt;
        }

        private static float TryReplaceFloat(float oldFloat, string newString)
        {
            if (Utils.TryConvertStringToFloat(newString, out float newFloat))
                return (oldFloat != newFloat) ? newFloat : oldFloat;

            Debug.Log($"Cannot convert {newString} to float!");
            return oldFloat;
        }

        private static Tenum TryReplaceFlagEnum<Tenum>(Tenum oldEnum, string newString) where Tenum : struct, Enum
        {
            Tenum newEnum = TryReplaceEnum<Tenum>("None");
            Tenum tempEnum;

            if (!newString.Contains('|'))
            {
                tempEnum = TryReplaceEnum<Tenum>(newString);
                return OrEnums(newEnum, tempEnum);
            }

            string[] flags = newString.Split('|');

            foreach (string flag in flags)
            {
                tempEnum = TryReplaceEnum<Tenum>(flag.Trim());
                newEnum = OrEnums(newEnum, tempEnum);
            }

            return TryReplaceEnum(oldEnum, newEnum);
        }

        private static Tenum TryReplaceEnum<Tenum>(Tenum oldEnum, string newString) where Tenum : struct, Enum
        {
            Tenum newEnum = TryReplaceEnum<Tenum>(newString);
            return TryReplaceEnum(oldEnum, newEnum);
        }

        private static Tenum TryReplaceEnum<Tenum>(string newString) where Tenum : struct, Enum
        {
            if (Enum.TryParse(newString, out Tenum newEnum))
                return newEnum;

            Debug.Log($"Cannot convert {newString} to enum!");
            return default;
        }

        private static Tenum TryReplaceEnum<Tenum>(Tenum oldEnum, Tenum newEnum) where Tenum : struct, Enum
        {
            if (!EqualityComparer<Tenum>.Default.Equals(oldEnum, newEnum))
                return newEnum;
            else
                return oldEnum;
        }

        private static Tenum OrEnums<Tenum>(Tenum a, Tenum b) where Tenum : struct, Enum
        {
            var underlyingType = Enum.GetUnderlyingType(typeof(Tenum));

            dynamic da = Convert.ChangeType(a, underlyingType);
            dynamic db = Convert.ChangeType(b, underlyingType);
            dynamic combined = da | db;

            return (Tenum)Enum.ToObject(typeof(Tenum), combined);
        }

        private static TSO TryReplaceSO<TSO, TListSO>(TSO oldSO, CSVType csvType, string newSOID, string soFolderPath) where TSO : BaseSO where TListSO : BaseListSO<TSO>
        {
            TListSO listSO = TryGetListSO<TSO, TListSO>(csvType, soFolderPath);

            if (Utils.TryConvertStringToInt(newSOID, out int soID))
            {
                TSO newSO = listSO.GetSOByID(soID);

                if (newSO != null)
                    return (oldSO != newSO) ? newSO : oldSO;
            }

            Debug.Log($"Cannot convert {newSOID} to SO!");
            return oldSO;
        }

        private static Sprite TryReplaceSpriteHead(Sprite oldSprite, CSVType csvType, string spriteName)
        {
            spriteName = spriteName.Replace("SO", "Head");
            return TryReplaceSprite(oldSprite, csvType, spriteName);
        }

        private static Sprite TryReplaceSprite(Sprite oldSprite, CSVType csvType, string spriteName)
        {
            spriteName = spriteName.Replace("SO", "");
            string spriteFilePath = $"Assets/Gallery/Images/{csvType}/{spriteName}.png";

            if (AssetDatabase.AssetPathExists(spriteFilePath))
            {
                Sprite newSprite = AssetDatabase.LoadAssetAtPath<Sprite>(spriteFilePath);
                return (oldSprite != newSprite) ? newSprite : oldSprite;
            }

            Debug.LogWarning($"Cannot convert {spriteFilePath} to sprite");
            return oldSprite;
        }

        private static Transform TryReplacePrefabTransform(Transform oldPrefabTransform, CSVType csvType, string prefabTransformName)
        {
            prefabTransformName = prefabTransformName.Replace("SO", "");
            string prefabTransformFilePath = $"Assets/Prefabs/{csvType}/{prefabTransformName}.prefab";

            if (AssetDatabase.AssetPathExists(prefabTransformFilePath))
            {
                Transform newPrefabTransform = AssetDatabase.LoadAssetAtPath<Transform>(prefabTransformFilePath);
                return (oldPrefabTransform != newPrefabTransform) ? newPrefabTransform : oldPrefabTransform;
            }

            Debug.LogWarning($"Cannot convert {prefabTransformFilePath} to prefab");
            return oldPrefabTransform;
        }

        private static IngredientStackListSO TryReplaceQuestObjectiveList(string name, string value, string soFolderPath)
        {
            string soName = $"{name}QuestObjectiveListSO";
            string islSOFilePath = soFolderPath + $"/IngredientStacks/" + soName + ".asset";

            IngredientListSO ingredientListSO = TryGetListSO<IngredientSO, IngredientListSO>(CSVType.Ingredients, soFolderPath);
            IngredientStackListSO questObjectiveList = LoadOrCreateSO<IngredientStackListSO>(islSOFilePath);
            questObjectiveList.name = soName;

            questObjectiveList.Clear();

            foreach (string qoJoined in value.Split('|'))
            {
                string[] qoSeparated = qoJoined.Split('_');

                if (int.TryParse(qoSeparated[0], out int ingredientID))
                {
                    IngredientSO ingredientSO = ingredientListSO.GetSOByID(ingredientID);

                    if (int.TryParse(qoSeparated[1], out int requiredAmount))
                    {
                        IngredientStackClass questObjective = new IngredientStackClass(ingredientSO, requiredAmount);
                        questObjectiveList.Add(questObjective);
                    }
                    else
                        Debug.LogError($"Cannot convert {qoSeparated[1]} to int");
                }
                else
                    Debug.LogError($"Cannot convert {qoSeparated[0]} to SO");
            }

            SaveSO(questObjectiveList);

            return questObjectiveList;
        }

        private static List<MonsterQTEStruct> TryReplaceMonsterQTEStructList(string value)
        {
            List<MonsterQTEStruct> monsterQTEList = new List<MonsterQTEStruct>();

            foreach (string mqteJoined in value.Split('|'))
            {
                string[] mqteSeparated = mqteJoined.Split('_');
                // Easy_3_4_1 | Medium_5_5_1 | Hard_6_6_2
                if (Enum.TryParse(mqteSeparated[0], out MonsterDifficulty monsterDifficulty))
                {
                    if (int.TryParse(mqteSeparated[1], out int qteCount))
                    {
                        if (int.TryParse(mqteSeparated[2], out int qteRange))
                        {
                            if (int.TryParse(mqteSeparated[3], out int qteReps))
                            {
                                monsterQTEList.Add(new MonsterQTEStruct(monsterDifficulty, qteCount, qteRange, qteReps));
                            }
                        }
                    }
                }

            }

            return monsterQTEList;
        }
        #endregion

        #region Save
        private static void SaveSO<TSO>(TSO so) where TSO : ScriptableObject
        {
            EditorUtility.SetDirty(so);
            AssetDatabase.SaveAssetIfDirty(so);
        }

        private static void TrySaveListSO<TBaseSO, TListSO>(TBaseSO baseSO, CSVType csvType, string soFolderPath) where TBaseSO : BaseSO where TListSO : BaseListSO<TBaseSO>
        {
            // Retrieve ListSO
            TListSO listSO = TryGetListSO<TBaseSO, TListSO>(csvType, soFolderPath);

            if (listSO.SOList.Count == 0)
                listSO.SOList.Add(baseSO);
            else
            {
                int soListIndex = listSO.GetIndex(baseSO);

                if (soListIndex > -1)
                    listSO[soListIndex] = baseSO;
                else
                    listSO.SOList.Add(baseSO);
            }

            SaveSO(listSO);
        }

        private static void TrySaveInventorySO(string soFolderPath)
        {
            string soName = "InventorySO";
            string islSOFilePath = soFolderPath + $"/IngredientStacks/" + soName + ".asset";

            IngredientListSO ingredientListSO = TryGetListSO<IngredientSO, IngredientListSO>(CSVType.Ingredients, soFolderPath);
            IngredientStackListSO inventorySO = LoadOrCreateSO<IngredientStackListSO>(islSOFilePath);
            inventorySO.name = soName;

            inventorySO.Clear();

            foreach (IngredientSO ingredientSO in ingredientListSO.SOList)
            {
                IngredientStackClass ingredientStack = new IngredientStackClass(ingredientSO, 0);
                inventorySO.Add(ingredientStack);
            }

            SaveSO(inventorySO);
        }
        #endregion
    }
}